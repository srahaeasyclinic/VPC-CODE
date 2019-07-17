import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutDetailViewComponent } from './layout-detail-view.component';

describe('LayoutDetailViewComponent', () => {
  let component: LayoutDetailViewComponent;
  let fixture: ComponentFixture<LayoutDetailViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutDetailViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutDetailViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
