import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutDetailViewActionComponent } from './layout-detail-view-action.component';

describe('LayoutfieldsComponent', () => {
  let component: LayoutDetailViewActionComponent;
  let fixture: ComponentFixture<LayoutDetailViewActionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutDetailViewActionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutDetailViewActionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
