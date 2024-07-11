import { Component, inject, OnInit } from '@angular/core';
import { NgbAccordionModule } from '@ng-bootstrap/ng-bootstrap';
import { EventsService } from '../../services/events.service';
import { UserEvent } from '../../models/events/userEvent';
import { ToastService } from '../../services/toast.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [NgbAccordionModule],
  templateUrl: './events.component.html',
  styleUrl: './events.component.scss'
})
export class EventsComponent implements OnInit {
  private eventsService = inject(EventsService);
  private toastService = inject(ToastService);
  private router = inject(Router);
  events: UserEvent[] = [];
  disabled = false;

  ngOnInit(): void {
    this.eventsService.getEvents()
      .subscribe(events => {
        this.events = events;
      });
  }

  register(eventId: number): void {
    this.disabled = true;
    this.eventsService.registerToEvent(eventId)
      .subscribe(_ => {
        this.toastService.show("Success", "You registered to the event");
        this.disabled = false;
      });
  }

  createEvent(): void {
    this.router.navigate(['/events/create']);
  }
}
