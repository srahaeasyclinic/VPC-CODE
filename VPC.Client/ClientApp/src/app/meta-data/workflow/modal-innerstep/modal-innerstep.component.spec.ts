import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalInnerStepComponent } from './modal-innerstep.component'

describe('ModalInnerStepComponent', () => {
  let component: ModalInnerStepComponent;
  let fixture: ComponentFixture<ModalInnerStepComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModalInnerStepComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalInnerStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
