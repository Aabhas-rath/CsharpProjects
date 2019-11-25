import { Injectable } from '@angular/core';
import {FilterModel} from '../models/filter.model';
import {PhraseCountFilterModel} from '../models/phrase-count-filter.model';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { FilterRangeBounds } from '../models/filterrange.model';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  // filter
  filter: FilterModel;
  
  // phrase count filter
  phraseCountFilter: PhraseCountFilterModel;
  
  constructor(private http: HttpClient) {
    this.initFilter();
  }
  
  /**
   * initialize filter
   */
  initFilter() {
    // init main filter
    this.filter = {
      api: '',
      states: [],
      basin: null,
      depth: {
        type: {
          label: 'Total vertical depth',
          value: 'total',
        },
        min: 0,
        max: 7500,
        unit: {
          label: 'Feet',
          value: 'ft',
        },
      },
      vintage: {
        min: 1900,
        max: 2019,
      },
      phrase: {
        category: {
          show: true,
          stain: true,
          trace: true,
          negative: true,
        },
        phrases: [],
        count: {
          min: 0,
          max: 100,
        },
        score: {
          min: 0,
          max: 500,
        }
      }
    };
    
    // init phrase count filter
    this.phraseCountFilter = {
      small: {
        min: 0,
        max: 10,
      },
      medium: {
        min: 11,
        max: 20,
      },
      large: {
        min: 21,
        max: 50,
      },
      xLarge: {
        min: 51,
        max: 10000,
      },
    };
  }

  getFilterBounds() {
    return this.http.get(API_URL + '/lookup/filter-range-bounds')
    .pipe(map((result: FilterRangeBounds) => {
      result.maxVintage = new Date().getFullYear();
      result.maxDepth = this.roundOffToNext(result.maxDepth, 100);
      result.maxPhraseCount = this.roundOffToNext(result.maxPhraseCount, 10);
      result.maxPhraseScore = this.roundOffToNext(result.maxPhraseScore, 100);

      return result;
    }));
  }
  
  /**
   * get filter
   */
  getFilter() {
    return this.filter;
  }

  roundOffToNext(val, roundOffTill) {
    return Math.ceil(val / roundOffTill) * roundOffTill;
  }
  
  /**
   * set filter
   * @param filter filter
   */
  setFilter(filter: FilterModel) {
    Object.assign(this.filter, filter);
  }
  
  /**
   * get phrase count filter
   */
  getPhraseCountFilter() {
    return this.phraseCountFilter;
  }
  
  /**
   * set phrase count filter
   * @param phraseCountFilter phraseCountFilter
   */
  setPhraseCountFilter(phraseCountFilter: PhraseCountFilterModel) {
    this.phraseCountFilter = phraseCountFilter;
  }
}
