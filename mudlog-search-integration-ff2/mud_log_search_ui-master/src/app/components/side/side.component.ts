import { Component, EventEmitter, OnInit, Output, Input, ViewChild } from '@angular/core';
import { StateService } from '../../services/state.service';
import { OptionItemModel } from '../../models/option-item.model';
import { PhraseService } from '../../services/phrase.service';
import { FormControl, FormGroup } from '@angular/forms';
import { PhraseFilterModel } from '../../models/phrase-filter.model';
import { BasinService } from '../../services/basin.service';
import { FilterModel } from '../../models/filter.model';
import { FilterService } from '../../services/filter.service';
import { CountyService } from 'src/app/services/county.service';
import { FieldService } from 'src/app/services/field.service';
import { PhrasetypeService } from 'src/app/services/phrasetype.service';
import { FilterRangeBounds } from 'src/app/models/filterrange.model';
import { SearchParams } from 'src/app/models/search.params.model';
import { ActivatedRoute } from '@angular/router';
import { SelectComponent } from '../select/select.component';
import { state } from '@angular/animations';
import { RangeSliderComponent } from '../range-slider/range-slider.component';

@Component({
  selector: 'app-side',
  templateUrl: './side.component.html',
  styleUrls: ['./side.component.scss']
})
export class SideComponent implements OnInit {

  @Input('stateSelector') stateSelector: SelectComponent;
  @Input('basinSelector') basinSelector: SelectComponent;

  @ViewChild('depthSliderComponent', {static: false}) depthSliderComponent: RangeSliderComponent;
  @ViewChild('phraseCountSliderComponent', {static: false}) phraseCountSliderComponent: RangeSliderComponent;
  @ViewChild('phraseScoreSliderComponent', {static: false}) phraseScoreSliderComponent: RangeSliderComponent;

  @Output() filterApply: EventEmitter<FilterModel> = new EventEmitter<FilterModel>();

  // depth list
  depthList: OptionItemModel[] = [];

  // state list
  stateList: OptionItemModel[] = [];

  // state list
  countyList: OptionItemModel[] = [];

  // unit list
  unitList: OptionItemModel[] = [];

  // phrase-type list
  phraseTypeList: OptionItemModel[] = [];
  phraseTypesLoaded = false;

  // phrase list
  phraseList: OptionItemModel[] = [];

  // basin list
  basinList: OptionItemModel[] = [];

  // field list
  fieldList: OptionItemModel[] = [];

  // filter data object
  data: FilterModel;

  // filter bounds
  filterBounds: FilterRangeBounds;

  // search params on this page
  searchParams: SearchParams;

  // phrase category form group
  phraseCategoryFormGroup: FormGroup = new FormGroup({
  });

  constructor(
    private stateService: StateService,
    private countyService: CountyService,
    private phraseTypeService: PhrasetypeService,
    private phraseService: PhraseService,
    private basinService: BasinService,
    private fieldService: FieldService,
    private filterService: FilterService,
    private activatedRoute: ActivatedRoute,
  ) {
  }

  ngOnInit() {
    const queryParams = this.activatedRoute.snapshot.queryParams;
    if (queryParams['lat1']) {
      this.searchParams = new SearchParams().fromQueryParams(queryParams);
    }
    this.initFilter(false);
    this.initFormGroupListener();
    this.getPhraseTypeList();
    this.getStateList();
    this.getBasinList();
    this.initUnitList();
    this.getPhraseList(this.phraseCategoryFormGroup.getRawValue());
  }

  /**
   * init filter data
   */
  initFilter(reset: boolean) {
    this.filterService.initFilter();
    this.data = this.filterService.getFilter();

    this.filterService.getFilterBounds().subscribe((response: FilterRangeBounds) => {
      this.filterBounds = response;
      this.data.depth.min = 0;
      this.data.depth.max = response.maxDepth;
      this.data.vintage.max = new Date().getFullYear();
      this.data.phrase.count.max = response.maxPhraseCount;
      this.data.phrase.score.max = response.maxPhraseScore;
      
      if (this.depthSliderComponent) {
        this.depthSliderComponent.updateValue(this.data.depth);
      }
      if (this.phraseCountSliderComponent) {
        this.phraseCountSliderComponent.updateValue(this.data.phrase.count);
      }
      if (this.phraseScoreSliderComponent) {
        this.phraseScoreSliderComponent.updateValue(this.data.phrase.score);
      }
      if (!reset && this.searchParams) {
        this.data = this.searchParams.updateFilter(this.data);
      }
    });

  }

  /**
   * init form group listener
   */
  private initFormGroupListener() {
    this.phraseCategoryFormGroup.valueChanges
      .subscribe((filters: PhraseFilterModel) => {
        this.data.phrase.category = filters;
        this.getPhraseList(filters);
      });
  }

  /**
   * set form group data
   */
  private setFormGroupData() {
    this.phraseCategoryFormGroup.setValue(this.data.phrase.category);
  }

  /**
   * initialize unit list
   */
  private initUnitList() {
    this.unitList = [
      {
        label: 'Feet',
        value: 'ft',
      },
      {
        label: 'Kilometer',
        value: 'km',
      },
    ];
  }

  /**
   * get state list
   */
  private getStateList() {
    this.stateService.getStateList()
      .subscribe((stateList: OptionItemModel[]) => {
        this.stateList = stateList;
      });
  }
  /**
   * get phraseType list
   */
  private getPhraseTypeList() {
    this.phraseTypeService.getPhraseTypes()
      .subscribe((phraseTypeList: OptionItemModel[]) => {
        this.phraseTypeList = phraseTypeList;
        this.phraseTypeList.forEach(type => {
          this.phraseCategoryFormGroup.addControl(type.value, new FormControl(true));
        });
        this.phraseTypesLoaded = true;
      });
  }

  public stateSelected(states: OptionItemModel[]) {
    console.log('stateSelected', states);
    if (states.length === 0) {
      return;
    }
    this.data.basin = undefined;
    const values = states.map(each => each.value).join(',');

    this.countyService.getCountyListForState(values)
      .subscribe((countyList: OptionItemModel[]) => {
        this.countyList = countyList;
      });

    this.fieldService.getFieldListForStates(values)
      .subscribe((fieldList: OptionItemModel[]) => {
        this.fieldList = fieldList;
      });
  }

  public basinSelected(basin: OptionItemModel) {
    if (basin && basin.value) {
      this.data.states.length = 0;
      this.data.county = undefined;
      this.countyList.length = 0;
      this.fieldService.getFieldListForBasin(basin.value)
        .subscribe((fieldList: OptionItemModel[]) => {
          this.fieldList = fieldList;
        });
    }
  }

  public countySelected(county: OptionItemModel) {
    if (county && county.value) {
      this.data.field = undefined;
    }
  }

  public fieldSelected(field: OptionItemModel) {
    if (field && field.value) {
      this.data.county = undefined;
    }
  }

  /**
   * get phrase list
   * @param filters filters
   */
  private getPhraseList(filters: PhraseFilterModel) {
    this.phraseService.getPhraseList(filters)
      .subscribe((phraseList: OptionItemModel[]) => {
        this.phraseList = phraseList;
        if (this.data.phrase.phrases.length === 0) {
          // filter based on category only when it is is not coming from the filtered-page
          this.data.phrase.phrases = this.data.phrase.phrases.filter((phrase) => {
            const index = this.phraseList.findIndex(item => item.value === phrase.value);

            return index !== -1;
          });
        }
      });
  }

  /**
   * get basin list
   */
  private getBasinList() {
    this.basinService.getBasinList()
      .subscribe((basinList: OptionItemModel[]) => {
        this.basinList = basinList;
      });
  }

  /**
   * on file upload
   * @param $event event
   */
  onFileUpload($event) {
    const file = $event.target.files[0];

    this.readUploadedFile(file)
      .then((content: string) => {
        this.data.api = content;
      })
      .catch((e) => {
        console.error(e.message);
      });

    $event.target.value = null;
  }

  /**
   * read uploaded file
   * @param file file data
   */
  private readUploadedFile(file: File) {
    const reader = new FileReader();

    reader.readAsText(file);

    return new Promise((resolve, reject) => {
      reader.onload = (event) => {
        const content = (event.target as FileReader).result as string;
        let parts: string[] = content.split(/\n/);
        parts = parts.filter(each => {
          return each && each.trim() !== '';
        }).map(each => each.trim());
        resolve(parts.join(','));
      };

      reader.onerror = (error) => {
        reject(error);
      };
    });
  }

  /**
   * emit filter apply event
   */
  emitFilterApply() {
    this.changeKilometerToFeet();
    this.filterApply.emit(this.data);
  }

  /**
   * change kilometer to feet
   */
  private changeKilometerToFeet() {
    if (this.data.depth.unit.value === 'km') {
      const min = this.data.depth.min * 3280.84;
      const max = this.data.depth.max * 3280.84;

      this.data.depth.min = min < 15000 ? min : 15000;
      this.data.depth.max = max < 15000 ? max : 15000;
      this.data.depth.unit.label = 'Feet';
      this.data.depth.unit.value = 'ft';
    }
  }

  /**
   * set phrases range min max
   * @param swappedList swapped list
   */
  setRangeMinMax(swappedList: OptionItemModel[]) {
    this.data.phrase.phrases = swappedList.map((item: OptionItemModel) => {
      return {
        label: item.label,
        value: item.value,
        alias: '',
        range: {
          min: this.data.phrase.count.min,
          max: this.data.phrase.count.max,
        },
      };
    });
  }
}
