import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserEvent } from '../models/events/userEvent';
import { environment } from '../../environments/environment';
import { EventCreate } from '../models/events/eventCreate';

@Injectable({
  providedIn: 'root'
})
export class EventsService {
  private eventsUrl = environment.apiUrl + 'events';

  constructor(private http: HttpClient) { }

  getEvents(): Observable<UserEvent[]> {
    return this.http.get<UserEvent[]>(this.eventsUrl);
  }

  createEvent(event: EventCreate): Observable<UserEvent> {
    return this.http.post<UserEvent>(this.eventsUrl, event);
  }

  registerToEvent(eventId: number): Observable<void> {
    return this.http.post<void>(`${this.eventsUrl}/${eventId}/members`, null);
  }
}
