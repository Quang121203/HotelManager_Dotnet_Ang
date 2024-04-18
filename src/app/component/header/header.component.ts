import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { User } from 'src/app/models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  user$: Observable<User>;
  constructor(private store: Store<{ user: User }>) {
    this.user$ = store.select('user');
  }

}
