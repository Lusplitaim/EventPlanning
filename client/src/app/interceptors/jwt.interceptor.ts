import { HttpInterceptorFn } from '@angular/common/http';
import { User } from '../models/user';
import { inject } from '@angular/core';
import { take } from 'rxjs';
import { AccountService } from '../services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  let currentUser: User | undefined;

  const accountService = inject(AccountService);
  const token = accountService.getToken();

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: "Bearer " + token
      }
    });
  }
  /* accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
  if (currentUser) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${currentUser.token}`
      }
    });
  } */

  return next(req);
};