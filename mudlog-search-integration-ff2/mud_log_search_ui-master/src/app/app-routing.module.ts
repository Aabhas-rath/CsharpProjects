import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {FilterPageComponent} from './pages/filter-page/filter-page.component';
import {FilteredPageComponent} from './pages/filtered-page/filtered-page.component';


const routes: Routes = [
  {
    path: '',
    component: FilterPageComponent,
  },
  {
    path: 'filtered',
    component: FilteredPageComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
