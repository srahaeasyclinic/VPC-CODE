import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicklistNewComponent } from './picklist-new.component';

describe('PicklistNewComponent', () => {
  let component: PicklistNewComponent;
  let fixture: ComponentFixture<PicklistNewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicklistNewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicklistNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
