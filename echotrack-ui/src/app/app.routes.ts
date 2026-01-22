import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { FeedbackListComponent } from './components/feedback-list/feedback-list.component';

export const routes: Routes = [
  {
    path: 'login',
    component: DashboardComponent
  },
  {
    path: 'feedback',
    component: FeedbackListComponent,
    canActivate: [authGuard]
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];
