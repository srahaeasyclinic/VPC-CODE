import { TestBed } from '@angular/core/testing';

import { RoutelocalizationService } from './routelocalization.service';

describe('RoutelocalizationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RoutelocalizationService = TestBed.get(RoutelocalizationService);
    expect(service).toBeTruthy();
  });
});
