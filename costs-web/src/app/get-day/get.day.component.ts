import { Component } from '@angular/core';

import { HttpService } from '../_services/http.service';
import { Day } from '../_models/day';

@Component({
    selector: 'get-day',
    templateUrl: 'get.day.component.html',
    providers: [HttpService]
})
export class GetDayComponent {
    public savedDay: Day;
    public dateToFind: Date;

    constructor(private httpService: HttpService) {
    }

    public GetDay(): void {
        this.httpService
            .getDay(this.dateToFind)
            .subscribe((d: Day) => this.savedDay = d);
    }
}