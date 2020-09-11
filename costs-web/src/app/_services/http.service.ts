import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Day } from '../_models/day';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.dev';

@Injectable()
export class HttpService {

    constructor(private http: HttpClient) {
    }

    public postDay(day: Day): Observable<Day> {
        let dayAsString = JSON.stringify(day);
        let httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };

        return this.http.post<Day>(environment.daysEndpoint, dayAsString, httpOptions);
    }

    public getDay(date: Date): Observable<Day> {
        let result = this.http.get<Day>(environment.singleDayEndpoint, {
            params: {
                "date": date.toString()
            }
        });

        return result;
    }

    public deleteDay(dateToDelete: Date): Observable<JSON> {
        let result = this.http.delete<JSON>(environment.singleDayEndpoint, {
            params: {
                "date": dateToDelete.toString()
            }
        });

        return result;
    }

    public getPlotData(): Observable<JSON> {
        let result = this.http.get<JSON>(environment.plotDataEndpoint);
        return result;
    }

    public getMostExpensiveDay(): Observable<JSON> {
        let result = this.http.get<JSON>(environment.analyzer + "/max-day/")
        return result;
    }

    public getDayByDate(date: string): Observable<JSON> {
        let result = this.http.get<JSON>(environment.analyzer + "/day-by-date", {
            params: {
                ["date"]: date
            }
        });
        return result;
    }

    public getTotalDaysCount(): Observable<JSON> {
        let result = this.http.get<JSON>(environment.analyzer + "/total-days-count");
        return result;
    }

    public getDaysAverageTotal(): Observable<JSON> {
        let result = this.http.get<JSON>(environment.analyzer + "/days-total-average");
        return result;
    }
}