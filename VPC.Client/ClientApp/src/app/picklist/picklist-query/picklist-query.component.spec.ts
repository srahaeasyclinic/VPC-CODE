import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicklistQueryComponent } from './picklist-query.component';

describe('PicklistQueryComponent', () => {
  let component: PicklistQueryComponent;
  let fixture: ComponentFixture<PicklistQueryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicklistQueryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicklistQueryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
