import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from './_helpers/auth.guard';
import { EnterDayComponent } from './enter-day/enter.day.component';
import { GetDayComponent } from './get-day/get.day.component'
import { DeleteDayComponent } from './delete-day/delete.day.component';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component'
import { HomeComponent } from './home/home.component'
import { DataVisualizationComponent } from './data-visualization/data.visualization.component';
import { DaysAnalyzerComponent } from './days-analyzer/days-analyzer.component';

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'enter-day', component: EnterDayComponent, canActivate: [AuthGuard] },
    { path: 'get-day', component: GetDayComponent, canActivate: [AuthGuard] },
    { path: 'delete-day', component: DeleteDayComponent, canActivate: [AuthGuard] },
    { path: 'plot-days', component: DataVisualizationComponent, canActivate: [AuthGuard] },
    { path: 'days-analyzer', component: DaysAnalyzerComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: '**', redirectTo: '', canActivate: [AuthGuard] }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }