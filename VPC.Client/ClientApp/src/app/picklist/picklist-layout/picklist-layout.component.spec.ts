import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicklistLayoutComponent } from './picklist-layout.component';

describe('PicklistLayoutComponent', () => {
  let component: PicklistLayoutComponent;
  let fixture: ComponentFixture<PicklistLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicklistLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicklistLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
