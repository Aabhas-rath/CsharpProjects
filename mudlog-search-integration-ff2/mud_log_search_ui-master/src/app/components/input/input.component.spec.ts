import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InputComponent } from './input.component';
import {RouterTestingModule} from '@angular/router/testing';
import {HttpClientModule} from '@angular/common/http';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from '../../app-routing.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {
  MatButtonModule,
  MatCheckboxModule, MatChipsModule,
  MatIconModule,
  MatInputModule,
  MatSidenavModule,
  MatSliderModule, MatSortModule,
  MatTableModule
} from '@angular/material';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CdkTableModule} from '@angular/cdk/table';
import {ColorPickerModule} from 'ngx-color-picker';
import {AppComponent} from '../../app.component';
import {MapComponent} from '../map/map.component';
import {SideComponent} from '../side/side.component';
import {FieldComponent} from '../field/field.component';
import {SelectComponent} from '../select/select.component';
import {OptionComponent} from '../option/option.component';
import {RangeComponent} from '../range/range.component';
import {SwapListComponent} from '../swap-list/swap-list.component';
import {FieldSubHeaderComponent} from '../field-sub-header/field-sub-header.component';
import {ModalComponent} from '../modal/modal.component';
import {FilterPageComponent} from '../../pages/filter-page/filter-page.component';
import {FilteredPageComponent} from '../../pages/filtered-page/filtered-page.component';
import {FilteredTableComponent} from '../filtered-table/filtered-table.component';
import {PhraseSettingsComponent} from '../phrase-settings/phrase-settings.component';
import {ChipComponent} from '../chip/chip.component';

describe('InputComponent', () => {
  let component: InputComponent;
  let fixture: ComponentFixture<InputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
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
      ],
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
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
