import { TestBed } from '@angular/core/testing';

import { FilterService } from './filter.service';
import {HttpClientModule} from '@angular/common/http';

describe('FilterService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ]}));

  it('should be created', () => {
    const service: FilterService = TestBed.get(FilterService);
    expect(service).toBeTruthy();
  });
});
