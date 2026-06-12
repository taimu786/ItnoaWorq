import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth-module').then((m) => m.AuthModule),
  },
  {
    path: 'feed',
    loadChildren: () =>
      import('./features/feed/feed-module').then((m) => m.FeedModule),
  },
  {
    path: 'profile',
    loadChildren: () =>
      import('./features/profile/profile-module').then((m) => m.ProfileModule),
  },
  {
    path: '',
    redirectTo: 'auth/login',
    pathMatch: 'full',
  },
];