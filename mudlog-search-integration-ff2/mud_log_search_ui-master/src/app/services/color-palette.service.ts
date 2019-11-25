import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ColorPaletteService {
  // default color set
  private defaultColors = [
    '#006ed2',
    '#ffd500',
    '#D61600',
    '#6e9700',
    '#6f42c1',
    '#e83e8c',
    '#17a28b',
  ];
  
  constructor() {}
  
  /**
   * create color palette
   * @param phrases phrase strings
   */
  createColorPaletteWithPhrases(phrases: string[]) {
    return phrases.map((phrase: string, index: number) => {
      return {
        name: phrase,
        color: this.defaultColors[(index >= this.defaultColors.length) ? index % this.defaultColors.length : index],
      };
    });
  }
}
