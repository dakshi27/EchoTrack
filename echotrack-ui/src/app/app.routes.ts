import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
//import { LoginComponent } from './components/login/login.component';
//import { FeedbackComponent } from './components/feedback/feedback.component';

export const routes: Routes = [
  {
    path: 'login',
    //component: LoginComponent
  },
  {
    path: 'feedback',
   // component: FeedbackComponent,
    canActivate: [authGuard]
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];
