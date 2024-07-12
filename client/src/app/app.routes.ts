import { Routes } from '@angular/router';
import { EventsComponent } from './components/events/events.component';
import { authGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { EventCreatorComponent } from './components/event-creator/event-creator.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';

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
    { path: "login", component: LoginComponent },
    { path: "sign-up", component: SignUpComponent },
];
