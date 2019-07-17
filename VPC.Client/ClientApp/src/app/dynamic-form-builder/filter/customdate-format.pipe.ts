
import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { CustomDateConstant } from './customdate-constant';
//https://stackblitz.com/edit/angular-material-datepicker-format?embed=1&file=app/date.adapter.ts
@Pipe({
  name: 'dateFormat'
})
export class DateFormatPipe extends DatePipe implements PipeTransform {
  transform(value: any, args?: any): any {
    return super.transform(value, CustomDateConstant.DATE_FMT);
  }
}