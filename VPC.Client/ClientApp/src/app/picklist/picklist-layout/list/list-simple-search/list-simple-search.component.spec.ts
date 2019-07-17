import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSimpleSearchComponent } from './list-simple-search.component';

describe('ListSimpleSearchComponent', () => {
  let component: ListSimpleSearchComponent;
  let fixture: ComponentFixture<ListSimpleSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListSimpleSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListSimpleSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
