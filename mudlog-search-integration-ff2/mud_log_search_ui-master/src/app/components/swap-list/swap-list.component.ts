import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  Renderer2,
  ViewChild
} from '@angular/core';
import {OptionItemModel} from '../../models/option-item.model';

@Component({
  selector: 'app-swap-list',
  templateUrl: './swap-list.component.html',
  styleUrls: ['./swap-list.component.scss']
})
export class SwapListComponent implements OnInit, AfterViewInit {
  @Input() height: number;
  @Input() list: OptionItemModel[] = [];
  @Input() swappedList: OptionItemModel[] = [];
  @Output() swappedListChange: EventEmitter<OptionItemModel[]> = new EventEmitter<OptionItemModel[]>();
  @ViewChild('swapListContainerElement', {static: false}) private swapListContainerElement: ElementRef;
  
  // filter input value
  filterString = '';
  
  // selected left side items
  selectedLeftList: OptionItemModel[] = [];
  
  // selected right side items
  selectedRightList: OptionItemModel[] = [];

  constructor(
    private renderer: Renderer2,
  ) { }

  ngOnInit() {
  }
  
  ngAfterViewInit(): void {
    this.setHeightToContainer();
  }
  
  /**
   * set container height
   */
  private setHeightToContainer() {
    if (this.height && this.swapListContainerElement) {
      const el = this.swapListContainerElement.nativeElement;
      
      this.renderer.setStyle(el, 'grid-template-rows', `${this.height}px`);
    }
  }
  
  /**
   * get filtered list
   */
  getFilteredList() {
    return this.list.filter((item) => {
      const swappedValues = this.swappedList.map(swappedItem => swappedItem.value);
      const isSwapped = swappedValues.indexOf(item.value) !== -1;
      
      return (item.label.indexOf(this.filterString) !== -1 && !isSwapped);
    });
  }
  
  /**
   * toggle left side item selected state
   * @param item item
   */
  toggleLeftItemSelect(item: OptionItemModel) {
    const index = this.selectedLeftList.findIndex((selectedItem) => {
      return selectedItem.value === item.value;
    });
    
    if (index === -1) {
      this.selectedLeftList.push(item);
    } else {
      this.selectedLeftList.splice(index, 1);
    }
  }
  
  /**
   * toggle right side item selected state
   * @param item item
   */
  toggleRightItemSelect(item: OptionItemModel) {
    const index = this.selectedRightList.findIndex((selectedItem) => {
      return selectedItem.value === item.value;
    });
  
    if (index === -1) {
      this.selectedRightList.push(item);
    } else {
      this.selectedRightList.splice(index, 1);
    }
  }
  
  /**
   * whether left side is selected
   * @param item item
   */
  isLeftSelected(item: OptionItemModel) {
    const index = this.selectedLeftList.findIndex((selectedItem) => {
      return selectedItem.value === item.value;
    });
    
    return index !== -1;
  }
  
  /**
   * whether right side is selected
   * @param item item
   */
  isRightSelected(item: OptionItemModel) {
    const index = this.selectedRightList.findIndex((selectedItem) => {
      return selectedItem.value === item.value;
    });
    
    return index !== -1;
  }
  
  /**
   * swap list to left or right
   * @param target 'left' | 'right'
   */
  swapList(target: 'left' | 'right') {
    if (target === 'left') {
      this.removeSelectedFromSwapped(this.selectedRightList);
      this.selectedRightList = [];
    } else {
      this.addSelectedToSwapped(this.selectedLeftList);
      this.selectedLeftList = [];
    }
    
    this.emitSwappedListChange();
  }
  
  /**
   * remove selected from right side
   * @param selectedList selected right side list
   */
  private removeSelectedFromSwapped(selectedList: OptionItemModel[]) {
    const selectedValues = selectedList.map(item => item.value);
    
    this.swappedList = this.swappedList.filter((swappedItem) => {
      return selectedValues.indexOf(swappedItem.value) === -1;
    });
  }
  
  /**
   * add selected to right side
   * @param selectedList selected left side
   */
  private addSelectedToSwapped(selectedList: OptionItemModel[]) {
    const swappedValues = this.swappedList.map(item => item.value);
    
    selectedList.forEach((selectedItem) => {
      if (swappedValues.indexOf(selectedItem.value) === -1) {
        this.swappedList.push(selectedItem);
      }
    });
  }
  
  /**
   * emit swappedListChange event
   */
  emitSwappedListChange() {
    this.swappedListChange.emit(this.swappedList);
  }
}
