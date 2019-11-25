declare const ConicGradient;

import {
  AfterViewInit, ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Inject,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Output,
  Renderer2,
  SimpleChanges,
  ViewChild
} from '@angular/core';
import OSM from 'ol/source/OSM';
import View from 'ol/View';
import Map from 'ol/Map';
import { fromLonLat } from 'ol/proj';
import { defaults as defaultControls } from 'ol/control';
import { clientConfig } from '../../../../client.config';
import { WellListModel } from '../../models/well-list.model';
import TileLayer from 'ol/layer/Tile';
import Overlay from 'ol/Overlay';
import OverlayPositioning from 'ol/OverlayPositioning';
import { DOCUMENT } from '@angular/common';
import { PhraseCountFilterModel } from '../../models/phrase-count-filter.model';
import { MatcherService } from '../../services/matcher.service';
import { WellPhraseModel } from '../../models/well-phrase.model';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit, AfterViewInit, OnChanges, OnDestroy {
  @Input() focus: WellListModel;
  @Input() type: 'all' | 'filtered';
  @Input() wellList: WellListModel[] = [];
  @Input() phraseCountFilter: PhraseCountFilterModel;
  @Input() colorPalette: { name: string, color: string }[];
  @Output() focusChange: EventEmitter<WellListModel> = new EventEmitter<WellListModel>();
  @Output() phraseCountChange: EventEmitter<PhraseCountFilterModel> = new EventEmitter<PhraseCountFilterModel>();
  @Output() changeExtent: EventEmitter<{ extent: number[], center: number[] }> = new EventEmitter<{ extent: number[], center: number[] }>();
  @ViewChild('mapElement', { static: false }) private mapElement: ElementRef<HTMLDivElement>;
  @ViewChild('popupElement', { static: false }) private popupElement: ElementRef<HTMLDivElement>;
  @ViewChild('bubblePopupElement', { static: false }) private bubblePopupElement: ElementRef<HTMLDivElement>;

  // map object
  map: Map;

  // well list
  data: WellListModel = null;

  // popup will be hidden after 100ms
  willBeHidden = false;

  // whether show popup element
  showPopupElement = false;

  // whether show setting component
  showSettingComponent = false;

  constructor(
    private renderer: Renderer2,
    private matcherService: MatcherService,
    private changeDetector: ChangeDetectorRef,
    @Inject(DOCUMENT) private document,
  ) { }

  /**
   * create tile layer
   */
  private static createTileLayer() {
    return new TileLayer({
      source: new OSM(),
    });
  }

  /**
   * create map view
   */
  private static createMapView() {
    const lng = clientConfig.map.center.lng;
    const lat = clientConfig.map.center.lat;

    return new View({
      center: fromLonLat([lng, lat]),
      zoom: 5,
      minZoom: 3,
      maxZoom: 14,
    });
  }

  /**
   * create default control
   */
  private static createDefaultControl() {
    return defaultControls({
      attribution: false,
      rotate: false,
      zoom: false,
    });
  }

  /**
   * create default marker
   * @param item well data
   */
  private static createDefaultMarker(item: WellListModel) {
    return new Overlay({
      position: fromLonLat([item.place.lng, item.place.lat]),
      positioning: OverlayPositioning.CENTER_CENTER,
      className: 'overlay-markers',
      id: item.name,
    });
  }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.createMapObject();
    this.setMapEvent();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.addMarkersOnMap();
    this.showBubblePopup();
  }

  ngOnDestroy(): void {
    this.map.dispose();
    this.map = null;
  }

  /**
   * create map object
   */
  private createMapObject() {
    if (this.mapElement) {
      const target = this.mapElement.nativeElement;
      const tileLayer = MapComponent.createTileLayer();
      const view = MapComponent.createMapView();
      const controls = MapComponent.createDefaultControl();

      this.map = new Map({
        view,
        target,
        controls,
        layers: [
          tileLayer,
        ],
      });
    }
  }

  /**
   * add marker on map
   */
  addMarkersOnMap() {
    if (this.map) {
      const markers = this.wellList.map((wellItem) => {
        if (this.type === 'all') {
          return MapComponent.createDefaultMarker(wellItem);
        } else {
          return this.createChartedMarker(wellItem);
        }
      });

      this.removeMarkersFromMap();
      this.setMarkerHoverEvent(markers);

      if (this.type === 'filtered') {
        this.setMarkerClickEvent(markers);
      }

      markers.forEach((marker) => {
        this.map.addOverlay(marker);
      });

      this.map.renderSync();
    }
  }

  /**
   * set map move event
   */
  private setMapEvent() {
    this.map.on('moveend', () => {
      const map = {
        extent: this.map.getView().calculateExtent(),
        center: this.map.getView().getCenter(),
      };
      console.log('map extent changed', map);
      this.changeExtent.emit(map);
    });
  }

  /**
   * remove marker from map
   */
  private removeMarkersFromMap() {
    if (this.map) {
      const markers = this.map.getOverlays();

      while (markers.getLength() > 0) {
        this.map.removeOverlay(markers.getArray()[0]);
      }
    }
  }

  /**
   * set marker hover event
   * @param markers marker overlays
   */
  private setMarkerHoverEvent(markers: Overlay[]) {
    markers.forEach((marker) => {
      const el = marker['element'];
      const adjust = this.getMarkerAdjustWidth(el);

      el.onmouseover = () => {
        const pos = this.getMarkerPosition(marker);
        const data = this.getWellDataWithName(marker.getId() as string);

        this.showPopup(pos.top, pos.left, adjust, data);
      };

      el.onmouseleave = () => {
        this.hidePopup();
      };
    });
  }

  /**
   * get marker adjust px
   * @param el marker element
   */
  private getMarkerAdjustWidth(el: HTMLElement) {
    if (el.classList.contains('small')) {
      return 10 / 2;
    } else if (el.classList.contains('medium')) {
      return 15 / 2;
    } else if (el.classList.contains('large')) {
      return 20 / 2;
    } else if (el.classList.contains('x-large')) {
      return 25 / 2;
    } else {
      return 5;
    }
  }

  /**
   * get marker top, left position
   * @param marker marker overlay
   */
  private getMarkerPosition(marker: Overlay) {
    const el = marker['element'];

    return {
      top: parseFloat(getComputedStyle(el).getPropertyValue('top')),
      left: parseFloat(getComputedStyle(el).getPropertyValue('left')),
    };
  }

  /**
   * set marker click event to charted marker
   * @param markers marker overlays
   */
  private setMarkerClickEvent(markers: Overlay[]) {
    markers.forEach((marker) => {
      const el = marker['element'];

      el.onclick = () => {
        this.toggleFocus(marker.getId() as string);
      };
    });
  }

  /**
   * show description popup
   * @param top top px
   * @param left left px
   * @param adjust adjust px
   * @param data data
   */
  private showPopup(top: number, left: number, adjust: number, data: WellListModel) {
    const popup = this.popupElement.nativeElement;

    this.data = data;
    this.willBeHidden = false;

    setTimeout(() => {
      const width = parseFloat(getComputedStyle(popup).getPropertyValue('width'));
      const height = parseFloat(getComputedStyle(popup).getPropertyValue('height'));

      this.renderer.setStyle(popup, 'left', `${left - (width / 2) + adjust}px`); // exclude half of popup width
      this.renderer.setStyle(popup, 'top', `${top - height - 10}px`); // exclude height so that show popup upward from marker
      this.showPopupElement = true;
    });
  }

  /**
   * hide description popup
   */
  hidePopup() {
    this.willBeHidden = true;

    setTimeout(() => {
      if (this.willBeHidden) {
        this.data = null;
        this.showPopupElement = false;
      }
    }, 100);
  }

  /**
   * get well data with name
   * @param name well name
   */
  private getWellDataWithName(name: string) {
    return this.wellList.filter((wellItem) => {
      return wellItem.name === name;
    })[0];
  }

  /**
   * force map to zoom in or out
   * @param state zoom state
   */
  zoom(state: 'in' | 'out') {
    const view = this.map.getView();
    const zoom = view.getZoom();

    if (state === 'in') {
      view.setZoom(zoom + 1);
    } else {
      view.setZoom(zoom - 1);
    }
  }

  /**
   * update map size
   * @description when drawer toggled, map should be resized
   */
  updateSize() {
    this.map.updateSize();
  }

  /**
   * create marker with chart
   * @param item well item
   */
  private createChartedMarker(item: WellListModel) {
    const className = this.getChartElementClass(item);

    return new Overlay({
      className,
      id: item.name,
      position: fromLonLat([item.place.lng, item.place.lat]),
      positioning: OverlayPositioning.CENTER_CENTER,
      element: this.createChartElement(item, className),
    });
  }

  /**
   * get chart element class by size
   * @param item well data
   */
  private getChartElementClass(item: WellListModel) {
    let count = 0;
    let size = null;

    item.phrase.phrases.forEach((phrase) => {
      count += phrase.count;
    });

    if (this.matcherService.matchMinMax(
      count,
      this.phraseCountFilter.xLarge.min,
      this.phraseCountFilter.xLarge.max
    )) {
      size = 'x-large';
    } else if (this.matcherService.matchMinMax(
      count,
      this.phraseCountFilter.large.min,
      this.phraseCountFilter.large.max
    )) {
      size = 'large';
    } else if (this.matcherService.matchMinMax(
      count,
      this.phraseCountFilter.medium.min,
      this.phraseCountFilter.medium.max
    )) {
      size = 'medium';
    } else if (this.matcherService.matchMinMax(
      count,
      this.phraseCountFilter.small.min,
      this.phraseCountFilter.small.max
    )) {
      size = 'small';
    }

    return `overlay-charted-markers ${size}`;
  }

  /**
   * create pie chart element
   * @param item well data
   * @param className class names
   */
  private createChartElement(item: WellListModel, className: string) {
    const chart: HTMLDivElement = this.document.createElement('div');
    const degs: { deg: number, phrase: string }[] = [];

    let total = 0;

    item.phrase.phrases.forEach((phrase: WellPhraseModel) => {
      total += phrase.count;
    });

    item.phrase.phrases.forEach((phrase: WellPhraseModel, index: number) => {
      const deg = (phrase.count / total) * 100;

      degs.push({
        deg: (index === 0) ? deg : degs[index - 1].deg + deg,
        phrase: phrase.value,
      });
    });

    const values = degs.map((deg: { deg: number, phrase: string }, index: number) => {
      const color = this.getColorFromPalette(deg.phrase);

      if (index === 0) {
        return `${color} ${deg.deg}%`;
      } else if (index !== degs.length - 1) {
        return `${color} 0 ${deg.deg}%`;
      } else {
        return `${color} 0`;
      }
    });

    if (values.length === 0) {
      chart.style.backgroundColor = '#fff';
    } else if (values.length === 1) {
      chart.style.backgroundColor = this.getColorFromPalette(degs[0].phrase);
    } else {
      const gradient = new ConicGradient({
        stops: values.join(', '),
        size: this.getSizeFromClassName(className),
      });

      gradient.canvas.style.display = 'block';

      chart.appendChild(gradient.canvas);
    }

    return chart;
  }

  /**
   * get size from class name
   * @param className class name
   */
  private getSizeFromClassName(className: string) {
    if (className.indexOf('x-large') !== -1) {
      return 25;
    } else if (className.indexOf('large') !== -1) {
      return 20;
    } else if (className.indexOf('medium') !== -1) {
      return 15;
    } else {
      return 10;
    }
  }

  /**
   * get color palette from service
   * @param phrase phrase string
   */
  private getColorFromPalette(phrase: string) {
    const color = this.colorPalette.filter(paletteItem => paletteItem.name === phrase)[0];

    return color ? color.color : '';
  }

  /**
   * show bubble popup to focused marker
   */
  private showBubblePopup() {
    if (!this.map) {
      return;
    }

    if (this.focus) {
      this.map.getOverlays().forEach((marker) => {
        if (this.focus.name === marker.getId()) {
          const element = this.bubblePopupElement.nativeElement.cloneNode(true) as HTMLElement;

          element.querySelector('.title').innerHTML = this.focus.name;

          this.map.addOverlay(new Overlay({
            element,
            position: marker.getPosition(),
            id: 'bubble-popup',
          }));

          this.map.renderSync();
        }
      });
    } else {
      this.map.renderSync();
    }
  }

  /**
   * set focused marker
   * @param name well name
   */
  setFocus(name: string) {
    this.focusChange.emit(this.getWellDataWithName(name));
  }

  /**
   * blur focused marker
   */
  setBlur() {
    this.focusChange.emit(null);
  }

  /**
   * toggle marker focus
   * @param name well name
   */
  toggleFocus(name: string) {
    if (this.focus && this.focus.name === name) {
      this.setBlur();
    } else {
      this.setFocus(name);
    }
  }

  /**
   * on apply phrase legend settings
   * @param phraseCountFilter phraseCountFilter
   */
  onApplySetting(phraseCountFilter: PhraseCountFilterModel) {
    this.phraseCountChange.emit(phraseCountFilter);
    this.onCancelSetting();
  }

  /**
   * on cancel phrase legend settings
   */
  onCancelSetting() {
    this.showSettingComponent = false;
  }

  /**
   * open phrase legend settings
   */
  openSettings() {
    this.showSettingComponent = true;
  }

  /**
   * Open url in a new tab for downloading
   * @param url the url
   */
  saveMudLog(url: string) {
    const parts = url.split('/');
    saveAs(url, parts[parts.length - 1]);
  }
}
