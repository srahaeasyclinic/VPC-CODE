import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalWorkflowprocessComponent } from './modal-workflowprocess.component';

describe('ModalWorkflowprocessComponent', () => {
  let component: ModalWorkflowprocessComponent;
  let fixture: ComponentFixture<ModalWorkflowprocessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModalWorkflowprocessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalWorkflowprocessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
