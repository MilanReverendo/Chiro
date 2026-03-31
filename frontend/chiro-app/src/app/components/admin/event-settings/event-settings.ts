import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EventDto } from '../../../models/event-dto';
import { EventService } from '../../../services/event-service';

@Component({
  selector: 'app-event-settings',
  imports: [CommonModule, FormsModule],
  templateUrl: './event-settings.html',
})
export class EventSettings implements OnInit {
  private eventService = inject(EventService);

  events = signal<EventDto[]>([]);
  isLoading = signal(true);
  showForm = signal(false);
  editingId = signal<string | null>(null);
  deleteConfirmId = signal<string | null>(null);

  form: EventDto = this.emptyForm();

  ngOnInit() { this.load(); }

  load() {
    this.isLoading.set(true);
    this.eventService.getAll().subscribe({
      next: (data) => { this.events.set(data); this.isLoading.set(false); },
      error: () => this.isLoading.set(false)
    });
  }

  openCreate() {
    this.form = this.emptyForm();
    this.editingId.set(null);
    this.showForm.set(true);
  }

  openEdit(event: EventDto) {
    this.form = { ...event };
    this.editingId.set(event.id!);
    this.showForm.set(true);
  }

  save() {
    const id = this.editingId();
    const op = id ? this.eventService.update(id, this.form) : this.eventService.create(this.form);
    op.subscribe(() => { this.showForm.set(false); this.load(); });
  }

  confirmDelete(id: string) { this.deleteConfirmId.set(id); }

  doDelete() {
    const id = this.deleteConfirmId();
    if (!id) return;
    this.eventService.delete(id).subscribe(() => { this.deleteConfirmId.set(null); this.load(); });
  }

  private emptyForm(): EventDto {
    return { id: undefined, name: '', description: '', startDate: '', endDate: '' };
  }
}
