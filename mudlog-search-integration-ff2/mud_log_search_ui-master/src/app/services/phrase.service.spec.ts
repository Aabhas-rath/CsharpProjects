import { TestBed } from '@angular/core/testing';

import { PhraseService } from './phrase.service';
import {HttpClientModule} from '@angular/common/http';

describe('PhraseService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ]}));

  it('should be created', () => {
    const service: PhraseService = TestBed.get(PhraseService);
    expect(service).toBeTruthy();
  });
});
