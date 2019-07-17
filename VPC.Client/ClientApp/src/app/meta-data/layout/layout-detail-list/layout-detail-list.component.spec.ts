import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutDetailListComponent } from './layout-detail-list.component';

describe('LayoutDetailListComponent', () => {
  let component: LayoutDetailListComponent;
  let fixture: ComponentFixture<LayoutDetailListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutDetailListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutDetailListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
