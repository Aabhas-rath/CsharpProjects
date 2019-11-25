import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class CountyService {

  constructor(
    private http: HttpClient,
  ) { }

  /**
   * get county list for the given states
   */
  getCountyListForState(states: string) {
    return this.http.get(API_URL + '/lookup/counties?states=' + states)
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
