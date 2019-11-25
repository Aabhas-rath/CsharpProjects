import { TestBed } from '@angular/core/testing';

import { ColorPaletteService } from './color-palette.service';
import {HttpClientModule} from '@angular/common/http';

describe('ColorPaletteService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ]}));

  it('should be created', () => {
    const service: ColorPaletteService = TestBed.get(ColorPaletteService);
    expect(service).toBeTruthy();
  });
});
