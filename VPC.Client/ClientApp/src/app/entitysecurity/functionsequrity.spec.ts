import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionSecurityComponent } from './functionsequrity.component';

describe('FunctionSecurityComponent', () => {
  let component: FunctionSecurityComponent;
  let fixture: ComponentFixture<FunctionSecurityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionSecurityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionSecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
