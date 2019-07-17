import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleSecurityComponent } from './rolesequrity.component';

describe('RoleSecurityComponent', () => {
  let component: RoleSecurityComponent;
  let fixture: ComponentFixture<RoleSecurityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoleSecurityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleSecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
