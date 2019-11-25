import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
  ViewChild,
  ViewEncapsulation
} from '@angular/core';
import { WellListModel } from '../../models/well-list.model';
import { MatSort, MatTableDataSource } from '@angular/material';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-filtered-table',
  templateUrl: './filtered-table.component.html',
  styleUrls: ['./filtered-table.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class FilteredTableComponent implements OnInit, OnChanges, AfterViewInit {
  @Input() focus: WellListModel;
  @Input() list: any[];
  @Output() focusChange: EventEmitter<WellListModel> = new EventEmitter<WellListModel>();
  @ViewChild(MatSort, { static: false }) private matSort: MatSort;

  // data source for table
  dataSource: MatTableDataSource<WellListModel>;

  aliases: string[] = ['cut', 'flor', 'stn', 'odor', 'strmg', 'resd', 'bleeding'];

  constructor(
    private changeDetector: ChangeDetectorRef,
  ) { }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.dataSource = new MatTableDataSource<WellListModel>([]);
    this.dataSource.sort = this.matSort;
    this.changeDetector.detectChanges();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.dataSource = new MatTableDataSource<WellListModel>(this.list.map((item) => {
      item.location = item.place.location;

      return item;
    }));

    this.dataSource.sort = this.matSort;
    this.changeDetector.detectChanges();
  }

  /**
   * toggle focus on row
   * @param row row
   */
  toggleFocus(row) {
    if (this.isFocused(row)) {
      this.setBlur();
    } else {
      this.setFocus(row);
    }
  }

  /**
   * set focus on row
   * @param row row
   */
  setFocus(row) {
    this.focusChange.emit(row);
  }

  /**
   * blur focused row
   */
  setBlur() {
    this.focusChange.emit(null);
  }

  /**
   * whether row is focused
   * @param row row
   */
  isFocused(row) {
    return row.name === (this.focus && this.focus.name);
  }

  private sum(items, prop) {
    if (items == null) {
      return 0;
    }
    return items.reduce((a, b) => {
      return b[prop] == null ? a : a + b[prop];
    }, 0);
  }

  /**
   * get phrase string array
   * @param row row
   */
  getPhraseStringArray(row: WellListModel) {
    return row.phrase.phrases.map(item => item.value).join(', ');
  }

  /**
   * get phrase string array for a alias
   * @param row row
   */
  getPhraseStringArrayForAlias(row: WellListModel, alias: string) {
    const aliasPhrases = row.phrase.phrases.filter(item => item.alias === alias);
    if (aliasPhrases.length === 0) {
      return;
    }
    const info = aliasPhrases.map(item => item.value).join(', ');

    const count = this.sum(aliasPhrases, 'count');
    let depthArrays: number[] = [];
    aliasPhrases.forEach(each => {
      depthArrays = depthArrays.concat(each.depth);
    });
    const maxDepth = Math.max(...depthArrays);
    const minDepth = Math.min(...depthArrays);
    return `Phrases: ${info}<br/>Count: ${count}<br/>Top: ${minDepth}<br/>Base: ${maxDepth}`;
  }

  getPhrasesOfAlias(row: WellListModel, alias: string) {
    return row.phrase.phrases
      .filter(item => item.alias === alias);
  }

  /**
   * get phrase depth array
   * @param depth depth list
   */
  getPhraseDepthArray(depth: number[]) {
    return depth.map(item => `${item}ft`).join(', ');
  }

  /**
   * view mud log event
   */
  viewMudLogs(url) {
    const parts = url.split('/');
    saveAs(decodeURI(url), parts[parts.length - 1]);
  }
}
