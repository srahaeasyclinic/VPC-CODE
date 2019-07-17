import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicklistListComponent } from './picklist-list.component';

describe('PicklistListComponent', () => {
  let component: PicklistListComponent;
  let fixture: ComponentFixture<PicklistListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicklistListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicklistListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
