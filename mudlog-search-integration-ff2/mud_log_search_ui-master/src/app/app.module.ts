import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MapComponent } from './components/map/map.component';
import {
  MatButtonModule,
  MatCheckboxModule, MatChipsModule,
  MatIconModule,
  MatInputModule, MatSelectModule,
  MatSidenavModule,
  MatSliderModule, MatSortModule,
  MatTableModule
} from '@angular/material';
import { SideComponent } from './components/side/side.component';
import { FieldComponent } from './components/field/field.component';
import { InputComponent } from './components/input/input.component';
import { SelectComponent } from './components/select/select.component';
import { OptionComponent } from './components/option/option.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { RangeComponent } from './components/range/range.component';
import { SwapListComponent } from './components/swap-list/swap-list.component';
import { FieldSubHeaderComponent } from './components/field-sub-header/field-sub-header.component';
import { ModalComponent } from './components/modal/modal.component';
import { FilterPageComponent } from './pages/filter-page/filter-page.component';
import { FilteredPageComponent } from './pages/filtered-page/filtered-page.component';
import {CdkTableModule} from '@angular/cdk/table';
import { FilteredTableComponent } from './components/filtered-table/filtered-table.component';
import { PhraseSettingsComponent } from './components/phrase-settings/phrase-settings.component';
import { ChipComponent } from './components/chip/chip.component';
import {ColorPickerModule} from 'ngx-color-picker';
import { RangeSliderComponent } from './components/range-slider/range-slider.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [
    AppComponent,
    MapComponent,
    SideComponent,
    FieldComponent,
    InputComponent,
    SelectComponent,
    OptionComponent,
    RangeComponent,
    SwapListComponent,
    FieldSubHeaderComponent,
    ModalComponent,
    FilterPageComponent,
    FilteredPageComponent,
    FilteredTableComponent,
    PhraseSettingsComponent,
    ChipComponent,
    RangeSliderComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatInputModule,
    MatIconModule,
    FormsModule,
    MatSliderModule,
    MatCheckboxModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatTableModule,
    CdkTableModule,
    MatSortModule,
    MatChipsModule,
    ColorPickerModule,
    MatSelectModule,
    NgxPaginationModule,
    NgxSpinnerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
