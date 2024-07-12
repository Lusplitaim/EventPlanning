import { Component, inject } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { FormControl, Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { EventCreate } from '../../models/events/eventCreate';
import { EventsService } from '../../services/events.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-event-creator',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './event-creator.component.html',
  styleUrl: './event-creator.component.scss'
})
export class EventCreatorComponent {
  formBuilder = inject(FormBuilder);
  eventsService = inject(EventsService);
  router = inject(Router);

  eventForm = this.formBuilder.group({
    name: new FormControl<string>('', [Validators.required]),
    description: new FormControl<string | undefined>(undefined),
    maxMembersCount: new FormControl<number | undefined>(undefined),
    startDate: new FormControl<Date | undefined>(undefined, [Validators.required]),
    endDate: new FormControl<Date | undefined>(undefined, [Validators.required]),
    isOnline: new FormControl<boolean>(false, [Validators.required]),
  });

  createEvent(): void {
    if (!this.eventForm.valid) {
      return;
    }

    let model: EventCreate = {
      name: this.getControlValue(EventFormKeys.Name),
      description: this.getControlValue(EventFormKeys.Description),
      maxMembersCount: this.getControlValue(EventFormKeys.MaxMembersCount),
      isOnline: this.getControlValue(EventFormKeys.IsOnline),
      startDate: this.getControlValue(EventFormKeys.StartDate),
      endDate: this.getControlValue(EventFormKeys.EndDate),
    };

    console.log(model);

    this.eventsService.createEvent(model)
      .subscribe(_ => {
        this.router.navigate([""]);
      });
  }

  private getControlValue(key: EventFormKeys) {
    return this.eventForm.get(this.formControlNames[key])!.value;
  }

  private formControlNames: Record<EventFormKeys, string> = {
    [EventFormKeys.Name]: "name",
    [EventFormKeys.Description]: "description",
    [EventFormKeys.StartDate]: "startDate",
    [EventFormKeys.EndDate]: "endDate",
    [EventFormKeys.IsOnline]: "isOnline",
    [EventFormKeys.MaxMembersCount]: "maxMembersCount",
  };
}

enum EventFormKeys {
  Name,
  Description,
  StartDate,
  EndDate,
  IsOnline,
  MaxMembersCount,
}
