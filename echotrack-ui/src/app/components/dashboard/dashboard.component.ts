import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent {

  feedbacks = [
    { id: 1, title: 'VPN Issue' },
    { id: 2, title: 'Login Bug' }
  ];

  onFeedbackSelected(id: number) {
    console.log('Selected feedback ID:', id);
  }
}
