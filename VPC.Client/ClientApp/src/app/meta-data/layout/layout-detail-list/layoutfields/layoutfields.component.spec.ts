import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutfieldsComponent } from './layoutfields.component';

describe('LayoutfieldsComponent', () => {
  let component: LayoutfieldsComponent;
  let fixture: ComponentFixture<LayoutfieldsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutfieldsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutfieldsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
