import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutDetailViewFieldsComponent } from './layout-detail-view-fields.component';

describe('LayoutfieldsComponent', () => {
  let component: LayoutDetailViewFieldsComponent;
  let fixture: ComponentFixture<LayoutDetailViewFieldsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutDetailViewFieldsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutDetailViewFieldsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
