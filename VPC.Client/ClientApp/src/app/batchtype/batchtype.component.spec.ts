import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchTypeComponent } from './batchtype.component';

describe('BatchTypeComponent', () => {
  let component: BatchTypeComponent;
  let fixture: ComponentFixture<BatchTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BatchTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BatchTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
