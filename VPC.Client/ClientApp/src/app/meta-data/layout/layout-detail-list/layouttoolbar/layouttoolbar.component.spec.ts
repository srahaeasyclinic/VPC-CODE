import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayouttoolbarComponent } from './layouttoolbar.component';

describe('LayouttoolbarComponent', () => {
  let component: LayouttoolbarComponent;
  let fixture: ComponentFixture<LayouttoolbarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayouttoolbarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayouttoolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
