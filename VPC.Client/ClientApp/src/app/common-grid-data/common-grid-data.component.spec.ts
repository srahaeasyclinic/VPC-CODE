import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonGridDataComponent } from './common-grid-data.component';

describe('CommonGridDataComponent', () => {
  let component: CommonGridDataComponent;
  let fixture: ComponentFixture<CommonGridDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CommonGridDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CommonGridDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
