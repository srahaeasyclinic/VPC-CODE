import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListFreeTextSearchComponent } from './list-free-text-search.component';

describe('ListFreeTextSearchComponent', () => {
  let component: ListFreeTextSearchComponent;
  let fixture: ComponentFixture<ListFreeTextSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListFreeTextSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListFreeTextSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
