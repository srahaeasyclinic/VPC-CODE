import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalOperationProcessComponent } from './modal-operationprocess.component';

describe('ModalOperationProcessComponent', () => {
  let component: ModalOperationProcessComponent;
  let fixture: ComponentFixture<ModalOperationProcessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModalOperationProcessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalOperationProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
