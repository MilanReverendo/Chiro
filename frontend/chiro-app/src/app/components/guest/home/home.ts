import { Component, OnInit, inject } from '@angular/core';
import { EventService } from '../../../services/event-service';
import { EventDto } from '../../../models/event-dto';
import { DatePipe, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-home',
  templateUrl: './home.html',
  styleUrl: './home.css',
  imports: [DatePipe, NgOptimizedImage],
})
export class Home implements OnInit {
  private eventService = inject(EventService);

  firstUpcomingEvent?: EventDto;
  isLoading = true;

  ngOnInit() {
    const now = new Date();
    this.eventService.getAll().subscribe({
      next: (events) => {
        const upcoming = events
          .filter((e) => new Date(e.startDate) >= now)
          .sort((a, b) => new Date(a.startDate).getTime() - new Date(b.startDate).getTime());

        this.firstUpcomingEvent = upcoming[0];
        this.isLoading = false;
      },
      error: () => (this.isLoading = false),
    });
  }
}
