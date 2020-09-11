import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'dateFormat'
})
export class DateFormatPipe implements PipeTransform {
    transform(value: Date, args?: any): string {
        const lastOneCharDigit = 9;
        
        let year = value.getFullYear().toString();

        var month = (value.getUTCMonth() + 1).toString();
        if (+month <= lastOneCharDigit) {
            month = "0" + month;
        }

        var day = value.getUTCDate().toString();;
        if (+day <= lastOneCharDigit) {
            day = "0" + day;
        }

        return year + "-" + month + "-" + day;
    }
}