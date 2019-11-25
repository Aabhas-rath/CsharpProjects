import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { WellListModel } from '../../models/well-list.model';
import { WellService } from '../../services/well.service';
import { FilterService } from '../../services/filter.service';
import { FilterModel } from '../../models/filter.model';
import { PhraseCountFilterModel } from '../../models/phrase-count-filter.model';
import { ColorPaletteService } from '../../services/color-palette.service';
import { PhraseService } from '../../services/phrase.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DOCUMENT } from '@angular/common';
import { Subscription } from 'rxjs';
import { toLonLat } from 'ol/proj';
import { OptionItemModel } from '../../models/option-item.model';
import { PaginatedWellList } from 'src/app/models/paginated.well-list.model';
import { FilterRangeBounds } from 'src/app/models/filterrange.model';
import { SearchParams } from 'src/app/models/search.params.model';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-filtered-page',
  templateUrl: './filtered-page.component.html',
  styleUrls: ['./filtered-page.component.scss']
})
export class FilteredPageComponent implements OnInit {
  // focus
  focus: WellListModel;

  // well list
  wellList: WellListModel[] = [];

  // filter
  filter: FilterModel;

  // phrase-aliases
  phraseAliases: string[];

  // phrase count filter
  phraseCountFilter: PhraseCountFilterModel;

  // color palette
  colorPalette: { name: string, color: string }[] = [];

  // whether show criteria element
  showCriteriaElement = false;

  // chip list
  chipList: { type: string, value: string, label: string}[] = [];

  // whether show phrase count detail element
  showPhraseCountDetailElement = false;

  // well list subscription
  subscription: Subscription;

  columns: string[] = ['name', 'uwi', 'field', 'vintage', 'phrases'];

  searchParams: SearchParams;
  
  // filter bounds
  filterBounds: FilterRangeBounds;

  config = {
    currentPage: 1,
    itemsPerPage: 10,
    totalItems: 0
    };

  constructor(
    private wellService: WellService,
    private filterService: FilterService,
    private colorPaletteService: ColorPaletteService,
    private changeDetector: ChangeDetectorRef,
    private phraseService: PhraseService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    @Inject(DOCUMENT) private document,
  ) {
    this.filter = filterService.getFilter();
    this.phraseCountFilter = filterService.getPhraseCountFilter();
    this.createAndGetColorPalette();
  }

  ngOnInit() {
    const queryParams = this.activatedRoute.snapshot.queryParams;
    this.searchParams = new SearchParams().fromQueryParams(queryParams);
    this.getPhraseAliases();
    this.getWellList();
    this.changeDetector.detectChanges();
    this.filter = this.searchParams.updateFilter(this.filter);
    this.filterService.getFilterBounds().subscribe((response: FilterRangeBounds) => {
      this.filterBounds = response;
    });
  }

  /**
   * load phrase-aliases
   */
  getPhraseAliases() {
    this.phraseService.getPhraseAliases().subscribe((aliases: string[]) => {
      this.phraseAliases = this.phraseAliases;
    });
  }

  /**
   * create and get color palette
   */
  private createAndGetColorPalette() {
    this.colorPalette = this.colorPaletteService.createColorPaletteWithPhrases(this.filter.phrase.phrases.map(item => item.value));
  }

  /**
   * update filter criteria
   */
  updateFilterCriteria() {
    this.searchParams['minPhraseCount'] = this.filter.phrase.count.min;
    this.searchParams['maxPhraseCount'] = this.filter.phrase.count.max;
    this.searchParams['minPhraseScore'] = this.filter.phrase.score.min;
    this.searchParams['maxPhraseScore'] = this.filter.phrase.score.max;
    this.getWellList();
  }

  /**
   * get well list
   */
  getWellList() {
    if (this.subscription) {
      this.subscription.unsubscribe();
      this.subscription = null;
    }

    if (this.filter.map && this.filter.map.extent.length > 0) {
        this.addExtentInfoInParams(this.searchParams);
    }
    if (!this.searchParams.lat1 || !this.searchParams.lon1 || !this.searchParams.lat2 || !this.searchParams.lon2) {
      // do not search is map extent is not available
      return;
    }

    this.subscription = this.wellService.getFilteredWellList(this.searchParams,
      this.config.itemsPerPage * (this.config.currentPage - 1),
      this.config.itemsPerPage)
      .subscribe((result: PaginatedWellList) => {
        this.wellList = result.records;
        this.config.totalItems = result.total;
      }, (e) => {
        console.log(e.message);
      });
  }

  /**
   * handle pagination page change
   * @param newPage next page
   */
  pageChange(newPage: number) {
    this.config.currentPage = newPage;
    this.getWellList();
  }

  /**
   * add map extent info in params
   * @param params the searchParams
   */
  private addExtentInfoInParams(params: SearchParams) {
    const extent: number[] = this.filter.map.extent;
    const lonLat1 = toLonLat([extent[0], extent[1]]);
    const lonLat2 = toLonLat([extent[2], extent[3]]);
    params.lat1 = lonLat1[1];
    params.lon1 = lonLat1[0];
    params.lat2 = lonLat2[1];
    params.lon2 = lonLat2[0];
  }

  /**
   * on phrase count change
   * @param phraseCountFilter phraseCountFilter
   */
  onPhraseCountChange(phraseCountFilter: PhraseCountFilterModel) {
    this.filterService.setPhraseCountFilter(phraseCountFilter);
    this.phraseCountFilter = this.filterService.getPhraseCountFilter();
  }

  /**
   * create filter chip
   */
  createFilterChips() {
    this.chipList = [];

    this.chipList.push(...this.stringArrayToFilterChip('api', this.searchParams.uwis));

    if (this.searchParams.states) {
      this.chipList.push(...this.valueArrayToFilterChip('state', this.searchParams.states.split(',')));
    }

    if (this.searchParams.basin) {
      this.chipList.push({
        type: 'basin',
        value: 'basin' + this.searchParams.basin,
        label: `basin: ${this.searchParams.basin}`
      });
    }

    if (this.searchParams.county) {
      this.chipList.push({
        type: 'county',
        value: this.searchParams.county,
        label: `country: ${this.searchParams.county}`
      });
    }

    if (this.searchParams.field) {
      this.chipList.push({
        type: 'field',
        value: this.searchParams.field,
        label: `field: ${this.searchParams.field}`
      });
    }

    if (this.searchParams.minDepth > 0) {
      this.chipList.push({
        type: 'min-depth',
        value: `${this.searchParams.minDepth} ${this.filter.depth.unit.label}`,
        label: `min-depth: ${this.searchParams.minDepth}`
      });
    }

    if (this.searchParams.maxDepth < this.filterBounds.maxDepth) {
      this.chipList.push({
        type: 'max-depth',
        value: `${this.searchParams.maxDepth} ${this.filter.depth.unit.label}`,
        label: `max-depth: ${this.searchParams.maxDepth}`
      });
    }

    if (this.searchParams.minVintage > 1900) {
      this.chipList.push({
        type: 'min-vintage',
        value: '' + this.searchParams.minVintage,
        label: `min-vintage: ${this.searchParams.minVintage}`
      });
    }
    if (this.searchParams.maxVintage < new Date().getFullYear()) {
      this.chipList.push({
        type: 'max-vintage',
        value: '' + this.searchParams.maxVintage,
        label: `max-vintage: ${this.searchParams.maxVintage}`
      });
    }

    if (this.searchParams.minPhraseScore > 0) {
      this.chipList.push({
        type: 'min-phrase-count',
        value: '' + this.searchParams.minPhraseCount,
        label: `min-phrase-count: ${this.searchParams.minPhraseCount}`
      });
    }

    if (this.searchParams.maxPhraseCount < this.filterBounds.maxPhraseCount) {
      this.chipList.push({
        type: 'max-phrase-count',
        value: '' + this.searchParams.maxPhraseCount,
        label: `max-phrase-count: ${this.searchParams.maxPhraseCount}`
      });
    }

    if (this.searchParams.minPhraseScore > 0) {
      this.chipList.push({
        type: 'min-phrase-score',
        value: '' + this.searchParams.minPhraseScore,
        label: `min-phrase-score: ${this.searchParams.minPhraseScore}`
      });
    }

    if (this.searchParams.maxPhraseScore < this.filterBounds.maxPhraseScore) {
      this.chipList.push({
        type: 'max-phrase-score',
        value: '' + this.searchParams.maxPhraseScore,
        label: `max-phrase-score: ${this.searchParams.maxPhraseScore}`
      });
    }

    if (this.searchParams.phrases) {
      this.chipList.push(...this.valueArrayToFilterChip('phrase', this.searchParams.phrases.split(',')));
    }
  }

  /**
   * string array value to filter chip
   * @param type filter type
   * @param value value
   */
  private stringArrayToFilterChip(type: string, value: string) {
    if (value && value.trim() !== '') {
      return value.split(',').map((item: string) => {
        return {
          type,
          value: item.trim(),
          label: `${type}: ${item}`
        };
      });
    } else {
      return [];
    }
  }

  /**
   * string array value to filter chip
   * @param type filter type
   * @param option option
   */
  private valueArrayToFilterChip(type: string, option: string[]) {
    const options = (option instanceof Array) ? option : [option];

    return options.map((item: string) => {
      return {
        type,
        value: item.trim(),
        label: `${type}: ${item.trim()}`
      };
    });
  }

  /**
   * toggle criteria section
   */
  toggleCriteria() {
    this.showCriteriaElement = !this.showCriteriaElement;

    if (this.showCriteriaElement) {
      this.createFilterChips();
    }
  }

  /**
   * remove filter chip
   * @param item filter item
   */
  removeChip(item: { type: string, value: string }) {
    switch (item.type) {
      case 'api': {
        this.removeItemFromArrayInSearchParams('uwis', item.value);
        break;
      }
      case 'state': {
        this.removeItemFromArrayInSearchParams('states', item.value);
        break;
      }
      case 'basin': {
        this.searchParams.basin = null;
        break;
      }
      case 'county': {
        this.searchParams.county = '';
        break;
      }
      case 'field': {
        this.searchParams.field = '';
        break;
      }
      case 'min-depth': {
        this.searchParams.minDepth = 0;
        break;
      }
      case 'max-depth': {
        this.searchParams.maxDepth = this.filterBounds.maxDepth;
        break;
      }
      case 'min-vintage': {
        this.searchParams.minVintage = 1900;
        break;
      }
      case 'max-vintage': {
        this.searchParams.maxVintage = this.filterBounds.maxVintage;
        break;
      }
      case 'min-phrase-count': {
        this.searchParams.minPhraseCount = 0;
        break;
      }
      case 'max-phrase-count': {
        this.searchParams.maxPhraseCount = this.filterBounds.maxPhraseCount;
        break;
      }
      case 'min-phrase-score': {
        this.searchParams.minPhraseScore = 0;
        break;
      }
      case 'max-phrase-score': {
        this.searchParams.maxPhraseScore = this.filterBounds.maxPhraseScore;
        break;
      }

      case 'phrase': {
        this.removeItemFromArrayInSearchParams('phrases', item.value);
        break;
      }
    }

    this.getWellList();
  }

  /**
   * remove a string from string array in search params
   * @param key the property name in search params
   * @param itemToRemove item to remove
   */
  private removeItemFromArrayInSearchParams(key: string, itemToRemove: string) {
    const list = this.searchParams[key].split(',');
    const index = list.findIndex(api => api.trim() === itemToRemove.trim());

    list.splice(index, 1);
    this.searchParams[key] = list.join(', ');
    this.searchParams[key].trim();
  }

  /**
   * remove string filter
   * @param type filter type
   * @param item filter item
   */
  private removeStringFilter(type: string, item: { type: string, value: string }) {
    const list = this.filter[type].split(',');
    const index = list.findIndex(api => api.trim() === item.value.trim());

    list.splice(index, 1);

    this.filter[type] = list.join(', ');
    this.filter[type].trim();
  }

  /**
   * remove options filter
   * @param type filter type
   * @param item filter item
   */
  private removeOptionsFilter(type: string, item: { type: string, value: string }) {
    const list = this.filter[type];
    const index = list.findIndex((listItem) => {
      return listItem.label === item.value;
    });

    list.splice(index, 1);

    this.filter[type] = list;
  }

  /**
   * back to main
   */
  backToMain() {
    const queryParams = new SearchParams().fromFiler(this.filter);
    this.router.navigate([''], { queryParams });
  }

  /**
   * show phrase count detail
   */
  showPhraseCountDetail() {
    this.showPhraseCountDetailElement = true;
  }

  /**
   * hide phrase count detail
   */
  hidePhraseCountDetail() {
    this.showPhraseCountDetailElement = false;
  }

  /**
   * get color from palette
   * @param phrase phrase
   */
  getColorFromPalette(phrase: string) {
    return this.colorPalette.filter(color => color.name === phrase)[0].color;
  }

  /**
   * set color to palette
   * @param $event color
   * @param phrase phrase
   */
  setColorToPalette($event: string, phrase: string) {
    this.colorPalette.forEach((palette) => {
      if (palette.name === phrase) {
        palette.color = $event;
      }
    });
  }

  /**
   * export to csv file
   */
  exportToExcel(event) {
    event.stopPropagation();
    event.preventDefault();
    this.spinner.show();
    this.wellService.exportFilteredWellList(this.searchParams)
      .subscribe((blob) => {
        this.spinner.hide();
        if (navigator.appVersion.toString().indexOf('.NET') > 0) { // for IE browser
          window.navigator.msSaveBlob(blob, `phrases_report_${new Date().getTime()}.xlsx`);
        } else { // for chrome and firfox
          const link = document.createElement('a');
          link.href = window.URL.createObjectURL(blob);
          link.download = `phrases_report_${new Date().getTime()}.xlsx`;
          link.click();
        }
      }
        , (error) => console.log('Error downloading the file.'));

  }

  /**
   * on change extent
   * @param map map position object
   */
  onChangeExtent(map: { extent: number[], center: number[] }) {
    this.filter.map = map;
    this.getWellList();
  }

  /**
   * change common phrase count
   */
  changeCommonPhraseCount() {
    const min = this.filter.phrase.count.min;
    const max = this.filter.phrase.count.max;

    this.filter.phrase.phrases.forEach((phrase) => {
      phrase.range.min = min;
      phrase.range.max = max;
    });
  }
}
