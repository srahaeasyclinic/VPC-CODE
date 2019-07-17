import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EntitySecurityComponent } from './entitysequrity.component';

describe('EntitySecurityComponent', () => {
  let component: EntitySecurityComponent;
  let fixture: ComponentFixture<EntitySecurityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EntitySecurityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EntitySecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
