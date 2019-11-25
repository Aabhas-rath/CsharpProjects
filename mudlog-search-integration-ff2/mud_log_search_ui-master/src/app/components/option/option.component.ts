import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {OptionItemModel} from '../../models/option-item.model';

@Component({
  selector: 'app-option',
  templateUrl: './option.component.html',
  styleUrls: ['./option.component.scss']
})
export class OptionComponent implements OnInit {
  @Input() value: string;
  @Output() optionClick: EventEmitter<OptionItemModel> = new EventEmitter<OptionItemModel>();
  @ViewChild('labelElement', {static: false}) private labelElement: ElementRef;

  constructor(
    private elementRef: ElementRef,
  ) { }
  
  ngOnInit() {}
  
  /**
   * return elementRef
   */
  getElementRef() {
    return this.elementRef;
  }
  
  /**
   * return option
   */
  getOption() {
    return {
      label: this.getLabel(),
      value: this.getValue(),
    };
  }
  
  /**
   * return value
   */
  getValue() {
    return this.value;
  }
  
  /**
   * return label
   */
  getLabel() {
    if (this.labelElement) {
      const el = this.labelElement.nativeElement;
      
      return el.innerText;
    } else {
      return '';
    }
  }
  
  /**
   * emit option click
   */
  emitOptionClick() {
    const option: OptionItemModel = {
      label: this.getLabel(),
      value: this.value,
    };
    this.optionClick.emit(option);
  }
}
