import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PhraseFilterModel} from '../models/phrase-filter.model';
import {map} from 'rxjs/operators';
import {PhraseListModel} from '../models/phrase-list.model';
import { environment } from 'src/environments/environment';


const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class PhraseService {
  constructor(
    private http: HttpClient,
  ) { }

  /**
   * get phrase aliases
   * @param filters filters
   */
  getPhraseAliases() {
    return this.http.get(API_URL + '/lookup/phrase-aliases');
  }

  /**
   * get phrase list
   * @param filters filters
   */
  getPhraseList(filters: PhraseFilterModel) {
    return this.http.get(API_URL + '/lookup/phrases')
      .pipe(map((result: PhraseListModel[]) => {
        const list = this.createFilterList(filters);

        return result.filter((item: PhraseListModel) => {
          return list.indexOf(item.category) !== -1;
        })
        .sort((a, b) => a.phrase.localeCompare(b.phrase))
        .map((item: PhraseListModel) => {
          return {
            label: decodeURI(item.phrase),
            value: decodeURI(item.phrase),
          };
        });
      }));
  }

  /**
   * create filter list
   * @param filters filters
   * @description get o
   */
  private createFilterList(filters: PhraseFilterModel) {
    const list = [];

    Object.keys(filters).forEach((key: string) => {
      if (filters[key]) {
        list.push(key);
      }
    });

    return list;
  }
}
