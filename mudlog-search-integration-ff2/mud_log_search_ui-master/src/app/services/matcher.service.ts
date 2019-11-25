import { Injectable } from '@angular/core';
import {WellPhraseModel} from '../models/well-phrase.model';
import {PhraseItemModel} from '../models/phrase-item.model';
import {OptionItemModel} from '../models/option-item.model';

@Injectable({
  providedIn: 'root'
})
export class MatcherService {
  /**
   * match string to string array
   * @param value string
   * @param target match target (should be string that can be separated with comma)
   */
  matchStringToStringArray(value: string, target: string) {
    if (!target || target === '') {
      return true;
    } else {
      const list = target.split(',').map(item => item.trim().toLowerCase());
      
      return list.indexOf(value.toLowerCase()) !== -1;
    }
  }
  
  /**
   * match string to option array
   * @param value value
   * @param option option
   */
  matchStringToOptionArray(value: string, option: OptionItemModel | OptionItemModel[]) {
    const options = (option instanceof Array) ? option : [option];
    const list = options.filter((item) => {
      if (!item.value || item.value.trim() === '') {
        return true;
      } else {
        return item.value.trim() === value;
      }
    });
    
    return list.length > 0;
  }
  
  /**
   * match min/max with value
   * @param value value
   * @param min min
   * @param max max
   */
  matchMinMax(value: number, min: number, max: number) {
    return (min <= value && max >= value);
  }
  
  /**
   * match phrases filter
   * @param wellPhraseModels well list
   * @param phrases phrase string array
   * @param depth depth min/max
   */
  matchPhrase(
    wellPhraseModels: WellPhraseModel[],
    phrases: PhraseItemModel[],
    depth: { min: number, max: number },
  ) {
    const filteredPhrases: WellPhraseModel[] = [];
    
    for (const phrase of phrases) {
      for (const wellPhraseModel of wellPhraseModels) {
        // match phrase existence
        if (wellPhraseModel.value === phrase.value) {
          const matchedDepth: number[] = [];
          let wellPhraseCount = 0;
          
          // match depth
          wellPhraseModel.depth.forEach((wellPhraseDepth: number) => {
            if (this.matchMinMax(wellPhraseDepth, depth.min, depth.max)) {
              wellPhraseCount++;
              matchedDepth.push(wellPhraseDepth);
            }
          });
          
          // match count
          if (wellPhraseCount !== 0 && this.matchMinMax(wellPhraseCount, phrase.range.min, phrase.range.max)) {
            filteredPhrases.push({
              alias: phrase.alias,
              value: phrase.value,
              count: wellPhraseCount,
              depth: matchedDepth,
            });
          }
        }
      }
    }
    
    return filteredPhrases;
  }
}
