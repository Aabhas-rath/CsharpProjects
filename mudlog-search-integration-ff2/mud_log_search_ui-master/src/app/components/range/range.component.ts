import {
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';

export interface RangeValue {
  min: number;
  max: number;
}

@Component({
  selector: 'app-range',
  templateUrl: './range.component.html',
  styleUrls: ['./range.component.scss']
})
export class RangeComponent {
  @Input() min: number;
  @Input() max: number;
  @Input() value: RangeValue;
  @Output() valueChange: EventEmitter<RangeValue> = new EventEmitter<RangeValue>();

  /**
   * change value
   * @param type 'min' | 'max'
   * @param $event event
   */
  changeValue(type: 'min' | 'max', $event) {
    const value = parseInt($event, null);

    if (!$event
      || isNaN(value)
      || value < this.min
      || value > this.max
    ) {
      return;
    }

    this.value[type] = parseInt($event, null);
    this.emitValueChange();
  }

  /**
   * correct value
   * @param value value
   */
  correctValue(value: number) {
    if (value < this.min) {
      value = this.min;
    } else if (value > this.max) {
      value = this.max;
    }

    return value;
  }

  /**
   * emit min change
   */
  emitValueChange() {
    this.valueChange.emit(this.value);
  }
}
