import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicklistOperationComponent } from './picklist-operation.component';

describe('PicklistOperationComponent', () => {
  let component: PicklistOperationComponent;
  let fixture: ComponentFixture<PicklistOperationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicklistOperationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicklistOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
