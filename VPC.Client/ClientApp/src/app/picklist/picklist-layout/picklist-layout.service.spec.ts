import { TestBed, inject } from '@angular/core/testing';

import { App\picklist\picklistLayout\picklistLayoutService } from './app\picklist\picklist-layout\picklist-layout.service';

describe('App\picklist\picklistLayout\picklistLayoutService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [App\picklist\picklistLayout\picklistLayoutService]
    });
  });

  it('should be created', inject([App\picklist\picklistLayout\picklistLayoutService], (service: App\picklist\picklistLayout\picklistLayoutService) => {
    expect(service).toBeTruthy();
  }));
});
