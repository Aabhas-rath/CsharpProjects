import {OptionItemModel} from './option-item.model';

export class PhraseItemModel extends OptionItemModel {
  alias: string;
  range: {
    min: number;
    max: number;
  };
}
