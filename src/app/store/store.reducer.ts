import { createReducer, on } from '@ngrx/store';
import { login,logout } from './store.actions';
import { User } from '../models';

export const initialState: User = new User();


export const storeReducer = createReducer(
  initialState,
  on(login, (state, { user }) => ({
    ...user
  })),
  on(logout, (state) => new User())
);