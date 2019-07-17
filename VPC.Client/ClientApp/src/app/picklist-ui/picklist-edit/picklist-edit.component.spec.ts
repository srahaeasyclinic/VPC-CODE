import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicklistEditComponent } from './picklist-edit.component';

describe('PicklistEditComponent', () => {
  let component: PicklistEditComponent;
  let fixture: ComponentFixture<PicklistEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicklistEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicklistEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
