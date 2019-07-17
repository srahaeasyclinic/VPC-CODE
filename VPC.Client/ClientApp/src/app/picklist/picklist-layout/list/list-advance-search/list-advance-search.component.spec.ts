import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAdvanceSearchComponent } from './list-advance-search.component';

describe('ListAdvanceSearchComponent', () => {
  let component: ListAdvanceSearchComponent;
  let fixture: ComponentFixture<ListAdvanceSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListAdvanceSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAdvanceSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
