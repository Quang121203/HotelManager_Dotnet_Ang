import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { increment, decrement, reset } from './counter.actions';


@Component({
  selector: 'app-counter',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  count$: Observable<number>;


  constructor(private store: Store<{ counter: number }>) {
    this.count$ = store.select('counter');
    

  }
  increment() {
    this.store.dispatch(increment());
    this.count$.subscribe(value => console.log('Counter Value:', value));
  }

  decrement() {
    this.store.dispatch(decrement());
  }

  reset() {
    this.store.dispatch(reset());
  }
}
