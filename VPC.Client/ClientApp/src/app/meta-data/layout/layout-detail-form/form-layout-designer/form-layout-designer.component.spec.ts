import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormLayoutDesignerComponent } from './form-layout-designer.component';

describe('LayoutDetailFormComponent', () => {
  let component: FormLayoutDesignerComponent;
  let fixture: ComponentFixture<FormLayoutDesignerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormLayoutDesignerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormLayoutDesignerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
