import {PhraseItemModel} from './phrase-item.model';
import {OptionItemModel} from './option-item.model';

export class FilterModel {
  api?: string;
  states?: OptionItemModel[];
  county?: OptionItemModel;
  basin?: OptionItemModel;
  field?: OptionItemModel;
  depth?: {
    type: OptionItemModel;
    min: number;
    max: number;
    unit: OptionItemModel;
  };
  vintage?: {
    min: number;
    max: number;
  };
  phrase?: {
    category: {
      show: boolean;
      stain: boolean;
      trace: boolean;
      negative: boolean;
    };
    phrases: PhraseItemModel[];
    count: {
      min: number;
      max: number;
    };
    score: {
      min: number;
      max: number;
    };
  };
  map?: {
    extent: number[];
    center: number[];
  };
}
