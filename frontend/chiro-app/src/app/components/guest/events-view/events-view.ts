import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventService } from '../../../services/event-service';
import { EventDto } from '../../../models/event-dto';

@Component({
  selector: 'app-events-view',
  imports: [CommonModule],
  templateUrl: './events-view.html',
  styleUrl: './events-view.css',
})
export class EventsView implements OnInit {
  eventService = inject(EventService);
  events: EventDto[] = [];
  isLoading = true;
  error: string | null = null;

  ngOnInit() {
    this.eventService.getAll().subscribe({
      next: (data) => {
        this.events = data
          .filter(e => new Date(e.startDate) >= new Date())
          .sort((a, b) =>
            new Date(a.startDate).getTime() - new Date(b.startDate).getTime()
          );

        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Failed to load events.';
        this.isLoading = false;
      }
    });
  }
}
