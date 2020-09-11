import { Component } from '@angular/core';
import { HttpService } from '../_services/http.service';

@Component({
  selector: 'days-analyzer',
  templateUrl: './days-analyzer.component.html',
  styleUrls: ['./days-analyzer.component.css'],
  providers: [HttpService]
})
export class DaysAnalyzerComponent {

  public options = ["Most Expensive Day", "Total entered days count",
    "Average days total", "Get day by date"];
  public option = "Choose here";
  public requestedData;
  public additionalInput = null;

  constructor(
    private http: HttpService,    
  ) { }

  public addAdditionInput(option: string) {
    this.additionalInput = null;
    switch (option) {
      case "Most Expensive Day": {
        return;
      }
      case "Get day by date": {
        this.additionalInput = "input for date";
      }
      case "Total entered days count": {
        return
      }
      case "Average days total": {
        return
      }
    }
  }

  public submitOptionRequest() {
    switch (this.option) {
      case "Most Expensive Day": {
        this.getMostExpensiveDay();
        break;
      }
      case "Get day by date": {
        this.getDayByDate();
        break;
      }
      case "Total entered days count": {
        this.getTotalDaysCount();
        break;
      }
      case "Average days total": {
        this.getDaysAverageTotal();
      }
    }
  }

  private getMostExpensiveDay(): void {
    this.http.getMostExpensiveDay().subscribe(it =>
      this.requestedData = it);
  }

  private getDayByDate(): void {
    let date = (<HTMLInputElement>document.querySelector("#date-input")).value;
    this.http.getDayByDate(date).subscribe(it =>
      this.requestedData = it);
  }

  private getTotalDaysCount(){
    this.http.getTotalDaysCount().subscribe(it =>
      this.requestedData = it);
  }

  private getDaysAverageTotal(){
    this.http.getDaysAverageTotal().subscribe(it =>
      this.requestedData = it);
  }
}