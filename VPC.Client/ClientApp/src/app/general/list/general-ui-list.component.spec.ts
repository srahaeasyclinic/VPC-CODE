import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralUiListComponent } from './general-ui-list.component';

describe('ListComponent', () => {
  let component: GeneralUiListComponent;
  let fixture: ComponentFixture<GeneralUiListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneralUiListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneralUiListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
