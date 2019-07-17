import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicklistRelationComponent } from './picklist-relation.component';

describe('PicklistRelationComponent', () => {
  let component: PicklistRelationComponent;
  let fixture: ComponentFixture<PicklistRelationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicklistRelationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicklistRelationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
