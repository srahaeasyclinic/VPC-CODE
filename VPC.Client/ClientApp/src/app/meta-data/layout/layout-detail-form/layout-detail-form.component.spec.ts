import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutDetailFormComponent } from './layout-detail-form.component';

describe('LayoutDetailFormComponent', () => {
  let component: LayoutDetailFormComponent;
  let fixture: ComponentFixture<LayoutDetailFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutDetailFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutDetailFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
