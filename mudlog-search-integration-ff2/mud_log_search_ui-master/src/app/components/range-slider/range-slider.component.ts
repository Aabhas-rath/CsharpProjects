import {
  AfterViewInit, ChangeDetectorRef,
  Component,
  ElementRef, EventEmitter,
  Inject,
  Input,
  OnChanges, Output,
  PLATFORM_ID,
  Renderer2,
  SimpleChanges,
  ViewChild
} from '@angular/core';
import {isPlatformBrowser} from '@angular/common';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-range-slider',
  templateUrl: './range-slider.component.html',
  styleUrls: ['./range-slider.component.scss']
})
export class RangeSliderComponent implements AfterViewInit, OnChanges {
  // value
  @Input() value: { min: number, max: number };
  // min value
  @Input() min: number;
  // max value
  @Input() max: number;
  // tick size
  // default is 1
  @Input() tick = 1;
  // min label
  @Input() minLabel: string;
  // max label
  @Input() maxLabel: string;
  // ignore overflow
  @Input() ignoreOverflow: boolean;
  // color
  @Input() color: string;
  // value change event emitter
  @Output() valueChange: EventEmitter<{ min: number, max: number }> = new EventEmitter<{ min: number, max: number }>();
  // slider element
  @ViewChild('sliderElement', {static: false}) private sliderElement: ElementRef;
  // min button element
  @ViewChild('minButtonElement', {static: false}) private minButtonElement: ElementRef;
  // max button element
  @ViewChild('maxButtonElement', {static: false}) private maxButtonElement: ElementRef;
  // thumb element
  @ViewChild('thumbElement', {static: false}) private thumbElement: ElementRef;

  debouncer: Subject<any> = new Subject();
  
  // total value (max - min)
  private total: number;
  // slider width
  private width: number;
  // slider left position on window
  private startPos: number;
  // total tick count (total / tick)
  private tickCount: number;
  // pixel per tick (width / tickCount)
  private pixelPerTick: number;
  // activated state
  private activated = false;
  // activated type
  private type: 'min' | 'max';
  
  constructor(
    private renderer: Renderer2,
    private changeDetector: ChangeDetectorRef,
    @Inject(PLATFORM_ID) private platformId,
  ) {
    this.debouncer
        .pipe(debounceTime(500))
        .subscribe((val) => this.valueChange.emit(val));
  }
  
  ngAfterViewInit(): void {
    this.getStaticValues();
    this.getSliderStatus();
    
    this.setMinPosition(this.value.min);
    this.setMaxPosition(this.value.max);
    
    this.setSliderThumbPosition();
  
    this.setColor();
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    this.getStaticValues();
    this.getSliderStatus();
    
    this.setMinPosition(this.value.min);
    this.setMaxPosition(this.value.max);
    
    this.setSliderThumbPosition();
    
    this.setColor();
  }
  
  detectChange() {
    this.getStaticValues();
    this.getSliderStatus();
  
    this.setMinPosition(this.value.min);
    this.setMaxPosition(this.value.max);
  
    this.setSliderThumbPosition();
  }
  
  /**
   * set color to slider
   */
  private setColor() {
    if (this.color && this.thumbElement && this.minButtonElement && this.maxButtonElement) {
      const thumb = this.thumbElement.nativeElement;
      const min = this.minButtonElement.nativeElement;
      const max = this.maxButtonElement.nativeElement;
      
      this.renderer.setStyle(thumb, 'background-color', this.color);
      this.renderer.setStyle(min, 'background-color', this.color);
      this.renderer.setStyle(max, 'background-color', this.color);
    }
  }
  
  /**
   * get total
   * @description
   * call on init and changes
   * calculate total and tickCount
   */
  private getStaticValues() {
    this.total = this.max - this.min;
    this.tickCount = this.total / this.tick;
  }
  
  /**
   * get slider status
   * @description
   * call on init and every slider activated
   * calculate slider width, startPos, endPos, tickPerPixel
   */
  private getSliderStatus() {
    if (this.sliderElement) {
      const el = this.sliderElement.nativeElement as HTMLElement;
      const rect = el.getBoundingClientRect();
      
      this.width = rect.width;
      this.startPos = rect.left;
      
      this.pixelPerTick = this.width / this.tickCount;
    }
  }
  
  /**
   * set activated type
   * @param type 'min' | 'max'
   */
  setActivatedType(type: 'min' | 'max') {
    this.type = type;
  }
  
  /**
   * activate slider
   */
  activateSlider() {
    this.activated = true;
    
    this.getStaticValues();
    this.getSliderStatus();
    
    if (isPlatformBrowser(this.platformId)) {
      window.onmouseup = () => {
        this.deactivateSlider();
      };
    }
  }
  
  /**
   * deactivate slider
   */
  deactivateSlider() {
    this.activated = false;
    
    if (isPlatformBrowser(this.platformId)) {
      window.onmouseup = null;
    }
  }
  
  /**
   * move slider
   * @param $event mouseEvent
   */
  moveSlider($event) {
    if (this.activated) {
      // current mouse position on slider
      const pos = $event.clientX - this.startPos;
      // position to tick size
      // use rounded value
      const posToTick = Math.round(pos / this.pixelPerTick);
      // value should corrected with min value
      let value = this.min + (this.tick * posToTick);
      
      if (value > this.max) {
        value = this.max;
      } else if (value < this.min) {
        value = this.min;
      }
      
      if (this.type === 'min') {
        this.setMinPosition(value);
      } else {
        this.setMaxPosition(value);
      }
      
      this.setSliderThumbPosition();
      // this.emitValueChange();
      this.debouncer.next(this.value);
    }
  }

  /**
   * update value of the slider with min and max
   * @param newVal the new min max values
   */
  updateValue(newVal: {min: number, max: number}) {
    this.setMinPosition(newVal.min);
    this.setMaxPosition(newVal.max);
    this.setSliderThumbPosition();
  }
  
  /**
   * set min button position
   * @param value min value
   */
  private setMinPosition(value: number) {
    if (this.minButtonElement) {
      if (value > this.value.max) {
        value = this.value.max;
      } else if (value < this.min) {
        value = this.min;
      }
      
      const el = this.minButtonElement.nativeElement;
      const per = this.getPositionWithValue(value);
      
      this.value.min = value;
      this.renderer.setStyle(el, 'left', `${per}%`);
    }
  }
  
  /**
   * set max button position
   * @param value max value
   */
  private setMaxPosition(value: number) {
    if (this.maxButtonElement) {
      if (value < this.value.min) {
        value = this.value.min;
      } else if (!this.ignoreOverflow && value > this.max) {
        value = this.max;
      }
      
      const el = this.maxButtonElement.nativeElement;
      let per = this.getPositionWithValue(value);
      
      if (this.ignoreOverflow && per > 100) {
        per = 100;
      }
      
      this.value.max = value;
      this.renderer.setStyle(el, 'left', `${per}%`);
    }
  }
  
  /**
   * get percentage of the current value
   * @param value min or max value
   */
  private getPositionWithValue(value: number) {
    return ((value - this.min) / this.total) * 100;
  }
  
  
  /**
   * set slider thumb position
   * @description set left, width of the slider thumb
   */
  private setSliderThumbPosition() {
    if (this.thumbElement) {
      const el = this.thumbElement.nativeElement;
      const min = this.getPositionWithValue(this.value.min);
      let max = this.getPositionWithValue(this.value.max);
      
      if (this.ignoreOverflow && max > 100) {
        max = 100;
      }
      
      this.renderer.setStyle(el, 'left', `${min}%`);
      this.renderer.setStyle(el, 'width', `${max - min}%`);
    }
  }
  
  /**
   * emit value change
   */
  emitValueChange() {
    this.valueChange.emit(this.value);
  }
}
