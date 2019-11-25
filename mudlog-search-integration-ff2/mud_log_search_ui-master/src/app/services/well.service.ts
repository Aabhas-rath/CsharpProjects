import { Injectable } from '@angular/core';
import { WellListModel } from '../models/well-list.model';
import { toLonLat } from 'ol/proj';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class WellService {

  constructor(
    private http: HttpClient,
  ) { }

  /**
   * get well list without any filter except for map extent
   * @param filter filter
   */
  getWellListWithoutFilter(fiiltermap) {
    const params: string[] = [];
    if (fiiltermap && fiiltermap.extent) {
      const extent: number[] = fiiltermap.extent;
      const lonLat1 = toLonLat([extent[0], extent[1]]);
      const lonLat2 = toLonLat([extent[2], extent[3]]);
      params.push(`lat1=${lonLat1[1]}`);
      params.push(`lon1=${lonLat1[0]}`);
      params.push(`lat2=${lonLat2[1]}`);
      params.push(`lon2=${lonLat2[0]}`);
      if (params.length > 0) {
        const url = `${API_URL}/wells?${params.join('&')}`;
        return this.http.get(url)
          .pipe(map((result: WellListModel[]) => {
            const list = result.map((item) => {
              item.imageUrls.forEach(eachUrl => {
                eachUrl = decodeURI(eachUrl);
              });
              return item;
            });

            return list;
          }));
      }
    }
    return of([]);
  }

  /**
   * get well list with filters
   * @param filter filter
   */
  getFilteredWellList(params: {}, offset: number, limit: number) {
    params['offset'] = offset;
    params['limit'] = limit;

    if (params) {
      const url = `${API_URL}/wells/search`;
      return this.http.get(url, { params });
    }
    return of([]);
  }

  /**
   * Export the filtered well list in excel
   * @param params filter params
   */
  exportFilteredWellList(params: {}) {
    return this.http.get(`${API_URL}/wells/export`, { responseType: 'blob', params });
  }
}
