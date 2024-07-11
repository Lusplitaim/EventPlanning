import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

export const authGuard: CanActivateFn = (route, state): boolean => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  const isLoggedIn = accountService.isLoggedIn();
  if (!isLoggedIn) {
    router.navigate(['/login']);
  }
  return isLoggedIn;
};