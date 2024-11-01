import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: 'dashboard', loadComponent: () => import('@dashboard/components/dashboard/dashboard.component') },
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
];
