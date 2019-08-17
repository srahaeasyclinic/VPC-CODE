import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MetaDataConfigurer } from './metadataconfigurer.component';


describe('LayoutDetailListAtom', () => {
  let component: MetaDataConfigurer;
  let fixture: ComponentFixture<MetaDataConfigurer>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MetaDataConfigurer ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MetaDataConfigurer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
