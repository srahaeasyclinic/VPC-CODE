import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralUiNewComponent } from './GeneralUiNew.component';

describe('NewComponent', () => {
  let component: GeneralUiNewComponent;
  let fixture: ComponentFixture<GeneralUiNewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneralUiNewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneralUiNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
