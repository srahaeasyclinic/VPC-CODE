import { TestBed } from '@angular/core/testing';

import { BredcrumService } from './bredcrum.service';

describe('BredcrumService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BredcrumService = TestBed.get(BredcrumService);
    expect(service).toBeTruthy();
  });
});
