import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IntializeMetadataConfigurer } from './intializemetadataconfigurer.component';

describe('IntializeLayoutDetailListAtom', () => {
  let component: IntializeMetadataConfigurer;
  let fixture: ComponentFixture<IntializeMetadataConfigurer>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IntializeMetadataConfigurer ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IntializeMetadataConfigurer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
