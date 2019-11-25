import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class BasinService {
  constructor(
    private http: HttpClient,
  ) { }

  /**
   * get basin list
   */
  getBasinList() {
    return this.http.get(API_URL + '/lookup/basins')
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
