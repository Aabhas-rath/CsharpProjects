import {WellPlaceModel} from './well-place.model';
import {WellPhraseModel} from './well-phrase.model';

export class WellListModel {
  name: string;
  api: string;
  place: WellPlaceModel;
  operator: string;
  vintage: number;
  basin: string;
  depth: number;
  field: string;
  imageUrls: string[];
  phrase: {
    phrases: WellPhraseModel[];
    score: number;
  };
}
