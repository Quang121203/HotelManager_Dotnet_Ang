import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from '../models';
import { SnackbarService } from './snackbar.service';


export const authGuard: CanActivateFn = (route, state) => {

  const router: Router = inject(Router);
  const store: Store<{ user: User }> = inject(Store);
  const user$: Observable<User> = store.select('user');
  const snackService: SnackbarService = inject(SnackbarService);

  user$.subscribe(value => {
    if (value.email == "") {
      router.navigate(['/login']);
      snackService.openSnackBar("You must login first");
    }
  });

  return true;
};
