import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GroupDto } from '../models/group-dto';

@Injectable({ providedIn: 'root' })
export class GroupService {
  private http = inject(HttpClient);
  private base = '/api/group';

  getAll(): Observable<GroupDto[]>               { return this.http.get<GroupDto[]>(this.base); }
  getById(id: string): Observable<GroupDto>      { return this.http.get<GroupDto>(`${this.base}/${id}`); }
  create(dto: GroupDto): Observable<GroupDto>    { return this.http.post<GroupDto>(this.base, dto); }
  update(id: string, dto: GroupDto): Observable<GroupDto> { return this.http.put<GroupDto>(`${this.base}/${id}`, dto); }
  delete(id: string): Observable<void>           { return this.http.delete<void>(`${this.base}/${id}`); }
}
