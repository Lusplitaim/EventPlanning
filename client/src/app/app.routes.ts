import { Routes } from '@angular/router';
import { EventsComponent } from './components/events/events.component';
import { AboutComponent } from './components/about/about.component';
import { authGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { EventCreatorComponent } from './components/event-creator/event-creator.component';

export const routes: Routes = [
    {
        path: "",
        redirectTo: "events",
        pathMatch: "full",
    },
    {
        path: "",
        canActivate: [authGuard],
        children: [
            { path: "events", component: EventsComponent },
            { path: "events/create", component: EventCreatorComponent },
        ],
    },
    { path: "about", canActivate: [authGuard], component: AboutComponent },
    { path: "login", component: LoginComponent },
];
