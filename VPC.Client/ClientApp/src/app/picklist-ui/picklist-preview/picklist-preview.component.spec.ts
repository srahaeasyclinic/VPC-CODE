import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicklistPreviewComponent } from './picklist-preview.component';

describe('PicklistPreviewComponent', () => {
  let component: PicklistPreviewComponent;
  let fixture: ComponentFixture<PicklistPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicklistPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicklistPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
