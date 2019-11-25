import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class PhrasetypeService {

  constructor(
    private http: HttpClient,
  ) { }

  /**
   * get phrase type list
   */
  getPhraseTypes() {
    return this.http.get(API_URL + '/lookup/phrase-types')
      .pipe(map((result: string[]) => {
        const list = result.map((item) => {
          return {
            label: item,
            value: item,
          };
        });

        return list;
      }));
  }
}
