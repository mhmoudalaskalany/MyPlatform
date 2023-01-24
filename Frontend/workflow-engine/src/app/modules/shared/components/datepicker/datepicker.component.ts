import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MomentDateAdapter, MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material/core';
import moment from 'moment';

@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html',
  styleUrls: ['./datepicker.component.scss'],
  providers: [
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS }
  ]
})
export class DatepickerComponent implements OnInit {

  moment = moment;
  today = moment(new Date());

  @Input() formGroup: FormGroup | any;
  @Input() controlName = '';

  @Input() minDate = '';
  @Input() maxDate = '';
  @Input() label = 'Date';

  constructor() { }

  ngOnInit(): void { }

  setDateValue(event: any) {
    let dateYear = +moment(event.value['_d']).format('YYYY'),
      dateMonth = +moment(event.value['_d']).format('MM') - 1,
      dateDay = +moment(event.value['_d']).format('DD'),
      dateHours = +moment().locale('en').format('HH'),
      dateMinutes = +moment().locale('en').format('mm');

    const dateFormated = moment(new Date(dateYear, dateMonth, dateDay, dateHours, dateMinutes)).format();
    this.formGroup.get(this.controlName).setValue(dateFormated);
  }

  preventDefault(event: KeyboardEvent) {
    event.preventDefault();
  }
}
