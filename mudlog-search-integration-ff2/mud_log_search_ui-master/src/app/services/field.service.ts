import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';


const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class FieldService {

  constructor(
    private http: HttpClient,
  ) { }

  /**
   * get field list for a county
   */
  getFieldListForStates(states: string) {
    return this.http.get(API_URL + '/lookup/fields?states=' + states)
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

  /**
   * get field list for a basin
   */
  getFieldListForBasin(basin: string) {
    return this.http.get(API_URL + '/lookup/fields?basin=' + basin)
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
