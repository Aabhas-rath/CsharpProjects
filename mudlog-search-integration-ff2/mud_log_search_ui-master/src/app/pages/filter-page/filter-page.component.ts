import { Component, OnInit, ViewChild, Output } from '@angular/core';
import { MapComponent } from '../../components/map/map.component';
import { WellListModel } from '../../models/well-list.model';
import { WellService } from '../../services/well.service';
import { FilterModel } from '../../models/filter.model';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SearchParams } from 'src/app/models/search.params.model';

@Component({
  selector: 'app-filter-page',
  templateUrl: './filter-page.component.html',
  styleUrls: ['./filter-page.component.scss']
})
export class FilterPageComponent implements OnInit {

  @ViewChild(MapComponent, { static: false }) private mapComponent: MapComponent;

  // well list
  wellList: WellListModel[] = [];

  // whether sidebar opened
  opened = true;

  // sidebar animation check time interval
  timer = null;

  // wellList subscription
  subscription: Subscription;

  constructor(
    private router: Router,
    private wellService: WellService
  ) { }

  ngOnInit(): void {
    this.getWellList();
  }

  /**
   * get well list
   * @param filter filter
   */
  getWellList(filter?: FilterModel) {
    if (this.subscription) {
      this.subscription.unsubscribe();
      this.subscription = null;
    }
    if (!filter) {
      return;
    }
    this.subscription = this.wellService.getWellListWithoutFilter(filter.map)
      .subscribe((result: WellListModel[]) => {
        this.wellList = result;
      }, (e) => {
        console.log(e.message);
      });
  }

  /**
   * toggle opened state
   */
  toggleOpened() {
    this.opened = !this.opened;
  }

  /**
   * change map size
   * @description it is needed for adjusting map size when mat-content width is changed
   */
  changeMapSize() {
    if (!this.mapComponent) {
      return;
    }

    this.removeTimer();

    this.timer = setInterval(() => {
      this.mapComponent.updateSize();
    });
  }

  /**
   * remove timer
   */
  removeTimer() {
    if (this.timer) {
      clearInterval(this.timer);
      this.timer = null;
    }
  }

  /**
   * apply filter
   */
  onFilterApply(filter: FilterModel) {
    const queryParams = new SearchParams().fromFiler(filter);
    this.router.navigate(['filtered'], { queryParams });
  }

  /**
   * Handle change is map extent by panning/zoom-in/zoom-out
   * @param map the map
   */
  onChangeExtent(map: { extent: number[], center: number[] }) {
    this.getWellList({ map });
  }
}
