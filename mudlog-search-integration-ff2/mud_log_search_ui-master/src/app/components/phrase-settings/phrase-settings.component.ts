import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FilterService} from '../../services/filter.service';
import {PhraseCountFilterModel} from '../../models/phrase-count-filter.model';

@Component({
  selector: 'app-phrase-settings',
  templateUrl: './phrase-settings.component.html',
  styleUrls: ['./phrase-settings.component.scss']
})
export class PhraseSettingsComponent implements OnInit {
  @Output() applySetting: EventEmitter<PhraseCountFilterModel> = new EventEmitter<PhraseCountFilterModel>();
  @Output() cancelSetting: EventEmitter<void> = new EventEmitter<void>();
  
  // phrase count filter
  phraseCountFilter: PhraseCountFilterModel;
  
  constructor(
    private filterService: FilterService,
  ) {
    this.phraseCountFilter = JSON.parse(JSON.stringify(filterService.getPhraseCountFilter()));
  }

  ngOnInit() {
  }
  
  /**
   * check validation
   */
  checkValidation() {
    const filter = this.phraseCountFilter;
    
    return (filter.medium.min === filter.small.max + 1
      && filter.large.min === filter.medium.max + 1
      && filter.xLarge.min === filter.large.max + 1);
  }
  
  /**
   * emit applySetting event
   */
  emitApplySetting() {
    if (this.checkValidation()) {
      this.applySetting.emit(this.phraseCountFilter);
    } else {
      alert('Please check count ranges');
    }
  }
  
  /**
   * emit cancelSetting event
   */
  emitCancelSetting() {
    this.cancelSetting.emit();
  }
}
