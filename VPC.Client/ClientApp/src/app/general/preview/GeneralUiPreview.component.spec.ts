import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralUiPreviewComponent } from './GeneralUiPreview.component';

describe('PreviewComponent', () => {
  let component: GeneralUiPreviewComponent;
  let fixture: ComponentFixture<GeneralUiPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneralUiPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneralUiPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
