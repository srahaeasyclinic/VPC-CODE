import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralUiEditComponent } from './GeneralUiEdit.component';

describe('EditComponent', () => {
  let component: GeneralUiEditComponent;
  let fixture: ComponentFixture<GeneralUiEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneralUiEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneralUiEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
