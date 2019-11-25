import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import {MapComponent} from './components/map/map.component';
import {HttpClientModule} from '@angular/common/http';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
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
import {SideComponent} from './components/side/side.component';
import {FieldComponent} from './components/field/field.component';
import {InputComponent} from './components/input/input.component';
import {SelectComponent} from './components/select/select.component';
import {OptionComponent} from './components/option/option.component';
import {RangeComponent} from './components/range/range.component';
import {SliderComponent} from './components/slider/slider.component';
import {SwapListComponent} from './components/swap-list/swap-list.component';
import {FieldSubHeaderComponent} from './components/field-sub-header/field-sub-header.component';
import {ModalComponent} from './components/modal/modal.component';
import {FilterPageComponent} from './pages/filter-page/filter-page.component';
import {FilteredPageComponent} from './pages/filtered-page/filtered-page.component';
import {FilteredTableComponent} from './components/filtered-table/filtered-table.component';
import {PhraseSettingsComponent} from './components/phrase-settings/phrase-settings.component';
import {ChipComponent} from './components/chip/chip.component';

describe('AppComponent', () => {
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
        SliderComponent,
        SwapListComponent,
        FieldSubHeaderComponent,
        ModalComponent,
        FilterPageComponent,
        FilteredPageComponent,
        FilteredTableComponent,
        PhraseSettingsComponent,
        ChipComponent,
      ],
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });
});
