import { TestBed } from '@angular/core/testing';

import { WellService } from './well.service';
import {HttpClientModule} from '@angular/common/http';

describe('WellService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ]
  }));

  it('should be created', () => {
    const service: WellService = TestBed.get(WellService);
    expect(service).toBeTruthy();
  });
});
