import {
  AfterViewInit,
  Component,
  ContentChildren,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  QueryList, Renderer2,
  SimpleChanges
} from '@angular/core';
import {OptionComponent} from '../option/option.component';
import {OptionItemModel} from '../../models/option-item.model';

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.scss']
})
export class SelectComponent implements OnInit, AfterViewInit, OnChanges {
  @Input() value: OptionItemModel | OptionItemModel[];
  @Input() multiple: boolean;
  @Input() placeholder: string;
  @Input() autocomplete: boolean;
  @Input() optionList: OptionItemModel[] = [];
  @Output() valueChange: EventEmitter<OptionItemModel | OptionItemModel[]> = new EventEmitter<OptionItemModel | OptionItemModel[]>();
  @ContentChildren(OptionComponent) optionComponents: QueryList<OptionComponent>;
  
  // filter keyword
  keyword = '';
  
  // whether focused
  focus = false;
  
  // filtered list when input
  filteredList: OptionItemModel[] = [];
  
  constructor(
    private renderer: Renderer2,
  ) {
    if (this.multiple) {
      this.value = [];
    }
  }
  
  ngOnInit() {}
  
  ngAfterViewInit(): void {}
  
  ngOnChanges(changes: SimpleChanges): void {
    setTimeout(() => {
      if (!this.focus) {
        this.setValue(this.value);
        this.initOptionComponents();
      }
    });
  }
  
  /**
   * emit value change
   */
  emitValueChange() {
    this.valueChange.emit(this.value);
  }
  
  /**
   * get selected label
   */
  getSelectedLabel() {
    const values = this.getValues();
    return values.map(value => value.label).join(', ');
  }
  
  /**
   * return values
   */
  private getValues() {
    const values = [];
    
    if (!this.value) {
      return values;
    }
  
    if (this.value instanceof Array) {
      values.push(...this.value);
    } else {
      values.push(this.value);
    }
    
    return values;
  }
  
  /**
   * filter optionComponents by input
   */
  filterOptions() {
    this.optionComponents.forEach((optionComponent) => {
      const option = optionComponent.getOption();
      const matched = option.label.toLowerCase().indexOf(this.keyword.toLowerCase()) !== -1;
      
      if (matched) {
        this.renderer.setStyle(optionComponent.getElementRef().nativeElement, 'display', 'block');
      } else {
        this.renderer.setStyle(optionComponent.getElementRef().nativeElement, 'display', 'none');
      }
    });
  }
  
  /**
   * initialize with optionComponent
   */
  private initOptionComponents() {
    if (this.optionComponents) {
      this.setOptionComponentsSubscriber();
    }
  }
  
  /**
   * set optionComponents subscriber
   */
  private setOptionComponentsSubscriber() {
    this.optionComponents.forEach((optionComponent) => {
      if (optionComponent.optionClick.observers.length === 0) {
        optionComponent.optionClick.subscribe((option: OptionItemModel) => {
          this.optionClicked(option);
        });
      }
    });
  }
  
  /**
   * set option component classes
   */
  private setOptionComponentClasses() {
    this.optionComponents.forEach((optionComponent) => {
      const option = {
        label: optionComponent.getLabel(),
        value: optionComponent.getValue(),
      };
      
      if (this.isSelected(option)) {
        this.renderer.addClass(optionComponent.getElementRef().nativeElement, 'selected');
      } else {
        this.renderer.removeClass(optionComponent.getElementRef().nativeElement, 'selected');
      }
    });
  }
  
  /**
   * option clicked event
   * @param option option
   */
  optionClicked(option: OptionItemModel) {
    if (this.multiple) {
      if (this.isSelected(option)) {
        this.removeOptionClicked(option);
      } else {
        this.addOptionClicked(option);
      }
      
      this.emitValueChange();
      this.setOptionComponentClasses();
    } else {
      this.focus = false;
      this.setValue(option);
    }
  }
  
  private removeOptionClicked(option: OptionItemModel) {
    const index = (this.value as OptionItemModel[]).findIndex(value => value.value === option.value);
    
    (this.value as OptionItemModel[]).splice(index, 1);
  }
  
  private addOptionClicked(option: OptionItemModel) {
    (this.value as OptionItemModel[]).push(option);
  }
  
  /**
   * set focus
   * @param focus focus state
   */
  setFocus(focus: boolean) {
    this.focus = focus;
    
    if (!this.focus) {
      // this.clearFilteredList();
      this.keyword = '';
      this.filterOptions();
    } else {
      this.setOptionComponentClasses();
    }
  }
  
  /**
   * set value
   * @param value value
   */
  setValue(value: OptionItemModel | OptionItemModel[]) {
    this.value = value;
    this.emitValueChange();
  }
  
  /**
   * check option is selected
   * @param option option
   */
  isSelected(option: OptionItemModel) {
    const values = this.getValues();
    const list = values.filter((value) => {
      return value.value === option.value;
    });
    
    return list.length > 0;
  }
  
  /**
   * check value is set
   */
  isValueSet() {
    if (this.multiple) {
      return (this.value as OptionItemModel[]).length > 0;
    } else {
      return !!this.value;
    }
  }
}
