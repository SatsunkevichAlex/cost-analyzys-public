import { Component } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { HttpService } from '../_services/http.service';

@Component({
    selector: 'data-visualization',
    templateUrl: './data.visualization.component.html',
    styleUrls: ['./data.visualization.component.css'],
    providers: [HttpService]
})
export class DataVisualizationComponent {
    plotData: JSON;
    view: any[] = [1500, 900];

    //options
    legend: boolean = true;
    showLabels: boolean = true;
    animations: boolean = true;
    xAxis: boolean = true;
    yAxis: boolean = true;
    showYAxisLabel: boolean = true;
    showXAxisLabel: boolean = true;
    xAxisLabel: string = "Day";
    yAxisLabel: string = "Sum";
    timeline: boolean = true;

    constructor(private http: HttpService) {
        this.http.getPlotData().subscribe(data =>
            this.plotData = data);
        Object.assign(this, { this: this.plotData });
    }

    onActivate(data): void {
        console.log('Activate', JSON.parse(
            JSON.stringify(this.plotData)
        ));
    }

    onDeactivate(data): void {
        console.log('Deactivate', JSON.parse(
            JSON.stringify(data)
        ));
    }

    onSelect(data): void {
        console.log('Item clicked', JSON.parse(JSON.stringify(data)));
      }
}