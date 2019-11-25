import { TestBed } from '@angular/core/testing';

import { StateService } from './state.service';
import {HttpClientModule} from '@angular/common/http';

describe('StateServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ]}));

  it('should be created', () => {
    const service: StateService = TestBed.get(StateService);
    expect(service).toBeTruthy();
  });
});
