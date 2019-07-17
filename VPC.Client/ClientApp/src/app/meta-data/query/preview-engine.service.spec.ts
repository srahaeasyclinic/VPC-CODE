import { TestBed, inject } from '@angular/core/testing';

import { PreviewEngineService } from './preview-engine.service';

describe('PreviewEngineService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PreviewEngineService]
    });
  });

  it('should be created', inject([PreviewEngineService], (service: PreviewEngineService) => {
    expect(service).toBeTruthy();
  }));
});
