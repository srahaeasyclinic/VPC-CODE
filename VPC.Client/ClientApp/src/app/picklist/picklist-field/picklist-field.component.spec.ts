import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicklistFieldComponent } from './picklist-field.component';

describe('PicklistFieldComponent', () => {
  let component: PicklistFieldComponent;
  let fixture: ComponentFixture<PicklistFieldComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicklistFieldComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicklistFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
