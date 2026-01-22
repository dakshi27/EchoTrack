import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-feedback-list',
  templateUrl: './feedback-list.component.html'
})
export class FeedbackListComponent {

  @Input() feedbacks: any[] = [];

  @Output() feedbackSelected = new EventEmitter<number>();

  selectFeedback(id: number) {
    this.feedbackSelected.emit(id);
  }
}
