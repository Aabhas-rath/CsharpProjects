import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class StateService {
  constructor(
    private http: HttpClient,
  ) { }

  /**
   * get state list from json
   */
  getStateList() {
    return this.http.get(API_URL + '/lookup/states')
      .pipe(map((result: string[]) => {
        const list = result.map((item) => {
          return {
            label: item,
            value: item,
          };
        });

        list.splice(0, 0, {
          label: 'All States',
          value: 'all-states',
        });

        return list;
      }));
  }
}
