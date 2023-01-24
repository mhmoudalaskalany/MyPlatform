import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatepickerHijriComponent } from './datepicker-hijri.component';

describe('DatepickerHijriComponent', () => {
  let component: DatepickerHijriComponent;
  let fixture: ComponentFixture<DatepickerHijriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatepickerHijriComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DatepickerHijriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
