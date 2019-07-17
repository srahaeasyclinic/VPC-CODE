import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkFlowSecurityComponent } from './workflowsequrity.component';

describe('WorkFlowSecurityComponent', () => {
  let component: WorkFlowSecurityComponent;
  let fixture: ComponentFixture<WorkFlowSecurityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkFlowSecurityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkFlowSecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
