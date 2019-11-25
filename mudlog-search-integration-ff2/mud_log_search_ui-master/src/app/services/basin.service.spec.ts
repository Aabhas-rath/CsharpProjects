import { TestBed } from '@angular/core/testing';

import { BasinService } from './basin.service';
import {HttpClientModule} from '@angular/common/http';

describe('BasinService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ]}));

  it('should be created', () => {
    const service: BasinService = TestBed.get(BasinService);
    expect(service).toBeTruthy();
  });
});
