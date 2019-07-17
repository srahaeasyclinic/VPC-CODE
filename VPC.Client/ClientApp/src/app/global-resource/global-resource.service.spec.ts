import { TestBed } from '@angular/core/testing';

import { GlobalResourceService } from './global-resource.service';

describe('GlobalResourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GlobalResourceService = TestBed.get(GlobalResourceService);
    expect(service).toBeTruthy();
  });
});
