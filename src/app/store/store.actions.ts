import { createAction, props } from '@ngrx/store';
import { User } from '../models';

export const login = createAction(
    'Login',
    props<{ user: User }>()
);

export const logout = createAction(
    'Logout'
);


