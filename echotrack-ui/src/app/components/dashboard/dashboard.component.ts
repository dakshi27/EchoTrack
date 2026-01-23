import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FeedbackListComponent } from '../feedback-list/feedback-list.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FeedbackListComponent],
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {

  feedbacks: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<any[]>('https://localhost:7252/api/Feedback')
      .subscribe({
        next: (data: any[]) => {
          this.feedbacks = data;
          console.log('API data:', data);
        },
        error: (err: any) => {
          console.error('API error:', err);
        }
      });
  }

  onFeedbackSelected(id: number) {
    console.log('Selected feedback ID:', id);
  }
}
