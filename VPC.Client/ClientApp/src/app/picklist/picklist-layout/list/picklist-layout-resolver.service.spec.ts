import { TestBed, inject } from '@angular/core/testing';

import { PicklistLayoutResolverService } from './picklist-layout-resolver.service';

describe('PicklistLayoutResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PicklistLayoutResolverService]
    });
  });

  it('should be created', inject([PicklistLayoutResolverService], (service: PicklistLayoutResolverService) => {
    expect(service).toBeTruthy();
  }));
});
