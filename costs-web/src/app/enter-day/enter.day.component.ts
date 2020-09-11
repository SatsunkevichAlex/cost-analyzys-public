import { Component, ViewChild, ElementRef } from '@angular/core';

import { Day } from '../_models/day';
import { KeyValuePair } from 'src/app/_helpers/keyValuePair';
import { HttpService } from '../_services/http.service';
import { ShopCategory } from '../_models/shopCategory';

@Component({
  selector: 'enter-day',
  templateUrl: './enter.day.component.html',
  styleUrls: ['./enter.day.component.css'],
  providers: [HttpService]
})
export class EnterDayComponent {
  public savedDay: Day;
  public date: Date;
  public itemCount: number = 1;
  public itemCost: number;
  public itemName: string;
  public days: Day[];
  public dayShops: KeyValuePair[];

  @ViewChild("entertainmentsChild", { static: false })
  private entertainmentsChild: ElementRef;
  @ViewChild("foodChild", { static: false })
  private foodChild: ElementRef;
  @ViewChild("householdChild", { static: false })
  private householdChild: ElementRef;

  constructor(private httpService: HttpService) {
    this.date = new Date();
    this.dayShops = [];
  }

  public SaveBuys(): void {
    var buys = document.querySelectorAll("table>tr[id]");
    for (let i = 0; i < buys.length; i++) {
      var buyName = buys[i].querySelector("#buy-name")['value'];
      var buyCost = +buys[i].querySelector("#buy-cost")['value'];
      var buyCount = +buys[i].querySelector("#buy-count")['value'];
      if (buyCount > 1) {
        let singleItemCost = buyCost / buyCount;
        for (let j = 0; j < buyCount; j++) {
          this.dayShops.push(new KeyValuePair(buyName, singleItemCost));
        }
      } else {
        this.dayShops.push(new KeyValuePair(buyName, buyCost));
      }
    }

    var enteredDay = this.CreateDay();
    this.SendDayToServer(enteredDay);
    this.dayShops = [];
  }

  public AddEntertainmentInput(): void {
    var table = (this.entertainmentsChild.nativeElement as HTMLElement);
    var tbRow = document.createElement("tr");
    tbRow.setAttribute('id', 'input-table-row');
    tbRow.innerHTML = `<label>Item</label>
    <td><input type='text' id='buy-name' [(ngModel)]="itemName"></td>
    <label>Cost</label>
    <td><input type='text' id='buy-cost' [(ngModel)]="itemCost"></td>
    <label>Count</label>
    <td><input type='text' id='buy-count' [(ngModel)]="itemCount" value='1'></td>`;
    table.appendChild(tbRow);
  }

  public AddFoodInput(): void {
    var table = (this.foodChild.nativeElement as HTMLElement);
    var tbRow = document.createElement("tr");
    tbRow.setAttribute('id', 'input-table-row');
    tbRow.innerHTML = `<label>Item</label>
    <td><input type='text' id='buy-name' [(ngModel)]="itemName"></td>
    <label>Cost</label>
    <td><input type='text' id='buy-cost' [(ngModel)]="itemCost"></td>
    <label>Count</label>
    <td><input type='text' id='buy-count' [(ngModel)]="itemCount" value='1'></td>`;
    table.appendChild(tbRow);
  }

  public AddHouseholdInput(): void {
    var table = (this.householdChild.nativeElement as HTMLElement);
    var tbRow = document.createElement("tr");
    tbRow.setAttribute('id', 'input-table-row');
    tbRow.innerHTML = `<label>Item</label>
    <td><input type='text' id='buy-name' [(ngModel)]="itemName"></td>
    <label>Cost</label>
    <td><input type='text' id='buy-cost' [(ngModel)]="itemCost"></td>
    <label>Count</label>
    <td><input type='text' id='buy-count' [(ngModel)]="itemCount" value='1'></td>`;
    table.appendChild(tbRow);
  }

  private CreateDay(): Day {
    let entertainmentsTotal = this.CalcCategoryTotal(ShopCategory.Entertainments);
    let foodTotal = this.CalcCategoryTotal(ShopCategory.Food);
    let householsTotal = this.CalcCategoryTotal(ShopCategory.Household);;
    let total = foodTotal + householsTotal + entertainmentsTotal;

    let day = new Day(
      {
        date: this.date,
        total: total,
        entertainments: entertainmentsTotal,
        food: householsTotal,
        household: householsTotal,
        shops: this.dayShops
      });

    return day;
  }

  private CalcCategoryTotal(shopCategory: ShopCategory): number {
    let result = 0;
    switch (+shopCategory) {
      case ShopCategory.Entertainments:
        var entertainmentsCosts = document.querySelectorAll("#entertainments-table #buy-cost");
        entertainmentsCosts.forEach(v => result += +(<HTMLInputElement>v).value);
        break;
      case ShopCategory.Food:
        var foodCosts = document.querySelectorAll("#food-table #buy-cost");        
        foodCosts.forEach(v => result += +(<HTMLInputElement>v).value);
        break;
      case ShopCategory.Household:
        var householdCosts = document.querySelectorAll("#household-table #buy-cost");
        householdCosts.forEach(v => result += +(<HTMLInputElement>v).value);
        break;
      default:
        console.error("Unrecognized shop category");        
    }
    return result;
  }

  private SendDayToServer(day: Day): void {
    this.httpService.postDay(day).subscribe((d: Day) => this.savedDay = d);    
  }
}