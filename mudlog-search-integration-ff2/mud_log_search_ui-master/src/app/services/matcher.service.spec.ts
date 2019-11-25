import { TestBed } from '@angular/core/testing';

import { MatcherService } from './matcher.service';
import {HttpClientModule} from '@angular/common/http';

describe('MatcherService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ]}));

  it('should be created', () => {
    const service: MatcherService = TestBed.get(MatcherService);
    expect(service).toBeTruthy();
  });
});
