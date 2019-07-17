import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RichtextboxComponent } from './richtextbox.component';

describe('RichtextboxComponent', () => {
  let component: RichtextboxComponent;
  let fixture: ComponentFixture<RichtextboxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RichtextboxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RichtextboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
