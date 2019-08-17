import { TestBed } from '@angular/core/testing';

import { SessionexpirypopupService } from './sessionexpirypopup.service';

describe('SessionexpirypopupService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SessionexpirypopupService = TestBed.get(SessionexpirypopupService);
    expect(service).toBeTruthy();
  });
});
