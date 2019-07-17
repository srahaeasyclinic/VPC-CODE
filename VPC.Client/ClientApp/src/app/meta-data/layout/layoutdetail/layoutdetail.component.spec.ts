import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutdetailComponent } from './layoutdetail.component';

describe('LayoutdetailComponent', () => {
  let component: LayoutdetailComponent;
  let fixture: ComponentFixture<LayoutdetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutdetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutdetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
