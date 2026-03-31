import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EventDto } from '../models/event-dto';

@Injectable({ providedIn: 'root' })
export class EventService {
  private http = inject(HttpClient);
  private base = '/api/event';

  getAll(): Observable<EventDto[]>               { return this.http.get<EventDto[]>(this.base); }
  getById(id: string): Observable<EventDto>      { return this.http.get<EventDto>(`${this.base}/${id}`); }
  create(dto: EventDto): Observable<EventDto>    { return this.http.post<EventDto>(this.base, dto); }
  update(id: string, dto: EventDto): Observable<EventDto> { return this.http.put<EventDto>(`${this.base}/${id}`, dto); }
  delete(id: string): Observable<void>           { return this.http.delete<void>(`${this.base}/${id}`); }
}
