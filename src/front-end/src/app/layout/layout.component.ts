import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from '../task/components/task-list/task-list.component';
import { TaskFormComponent } from '../task/components/task-form/task-form.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { loadTaskStatuses } from '../task/store/task-status/task-status.actions';
import { Task } from '../task/models/task.model';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    CommonModule,
    TaskListComponent,
    TaskFormComponent,
    MatToolbarModule,
    MatGridListModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent {
  cols$: Observable<number>;
  selectedTask: Task | null = null;

  constructor(breakpointObserver: BreakpointObserver, private store: Store) {
    this.cols$ = breakpointObserver.observe([Breakpoints.Handset]).pipe(
      map(result => (result.matches ? 1 : 2))
    );

    this.store.dispatch(loadTaskStatuses());
  }

  onLogout() {
    this.store.dispatch({ type: '[Auth] Logout' });
  }

  onTaskEdit(task: Task) {
    this.selectedTask = task;
  }

  onTaskFormReset() {
    this.selectedTask = null;
  }
}
