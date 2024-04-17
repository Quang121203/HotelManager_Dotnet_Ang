import { createAction } from '@ngrx/store';

export const increment = createAction('Increment');
export const decrement = createAction('Decrement');
export const reset = createAction('Reset');