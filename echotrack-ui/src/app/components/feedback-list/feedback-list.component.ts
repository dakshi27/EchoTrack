import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-feedback-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './feedback-list.component.html'
})
export class FeedbackListComponent {

  @Input() feedbacks: any[] = [];

  @Output() feedbackSelected = new EventEmitter<number>();

  selectFeedback(id: number) {
    this.feedbackSelected.emit(id);
  }
}
