import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxChartsModule } from '@swimlane/ngx-charts'

import { AppComponent } from './app.component';
import { EnterDayComponent } from './enter-day/enter.day.component';
import { GetDayComponent } from './get-day/get.day.component';
import { DeleteDayComponent } from './delete-day/delete.day.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { DataVisualizationComponent } from './data-visualization/data.visualization.component'

import { DateFormatPipe } from './_helpers/date.format.pipe';
import { AppRoutingModule } from './app.routing.module';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { DaysAnalyzerComponent } from './days-analyzer/days-analyzer.component';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        AppRoutingModule,
        HttpClientModule,
        ReactiveFormsModule,
        NgxChartsModule,
        BrowserAnimationsModule
    ],
    declarations: [
        AppComponent,
        DateFormatPipe,
        EnterDayComponent,
        GetDayComponent,
        DeleteDayComponent,
        LoginComponent,
        HomeComponent,
        DataVisualizationComponent,
        DaysAnalyzerComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }