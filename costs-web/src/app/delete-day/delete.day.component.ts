import { Component } from '@angular/core';

import { HttpService } from '../_services/http.service'

@Component({
    selector: 'delete-day',
    templateUrl: './delete.day.component.html',
    styleUrls: ['./delete.day.component.css'],
    providers: [HttpService]
})
export class DeleteDayComponent {    
    public dateToDelete: Date;
    public removeStatus: JSON;

    constructor(private httpService: HttpService) {
    }

    public DeleteDay(): void {
        this.httpService.deleteDay(this.dateToDelete)
            .subscribe(it => this.removeStatus = it);
    }
}