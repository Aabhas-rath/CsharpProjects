import { TestBed } from '@angular/core/testing';

import { PhrasetypeService } from './phrasetype.service';

describe('PhrasetypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PhrasetypeService = TestBed.get(PhrasetypeService);
    expect(service).toBeTruthy();
  });
});
