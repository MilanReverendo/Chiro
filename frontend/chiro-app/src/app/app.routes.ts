import { Routes } from '@angular/router';
import { Login } from './components/guest/login/login';
import { AdminDashboard } from './components/admin/admin-dashboard/admin-dashboard';
import { authGuard } from './guards/auth-guard';
import { Home } from './components/guest/home/home';
import { EventsView } from './components/guest/events-view/events-view';
import { GroupSettings } from './components/admin/group-settings/group-settings';
import { EventSettings } from './components/admin/event-settings/event-settings';
import { ProfileSettingsComponent } from './components/admin/profile-settings/profile-settings';
import { LeaderSettings } from './components/admin/leader-settings/leader-settings';
import { GroupsView } from './components/guest/groups-view/groups-view';
import { ContactPage } from './components/guest/contact/contact-page/contact-page';

export const routes: Routes = [
  // Public routes
  { path: '', component: Home },
  { path: 'login', component: Login },
  { path: 'evenementen', component: EventsView },
  { path: 'groepen', component: GroupsView },
  { path: 'contact', component: ContactPage },
  // Admin routes (protected)
  {
    path: 'admin',
    component: AdminDashboard,
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'profiel', pathMatch: 'full' }, // default route
      { path: 'groepen', component: GroupSettings },
      { path: 'evenementen', component: EventSettings },
      { path: 'leiding', component: LeaderSettings },
      { path: 'profiel', component: ProfileSettingsComponent },
    ]
  },

  // Fallback
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
