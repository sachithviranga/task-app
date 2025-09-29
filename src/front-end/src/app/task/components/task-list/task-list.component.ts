import {
  Component,
  Output,
  EventEmitter,
  OnInit,
  OnDestroy
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { Store } from '@ngrx/store';
import {
  Observable,
  Subject,
  BehaviorSubject,
  combineLatest,
  map,
  takeUntil
} from 'rxjs';
import { Task } from '../../models/task.model';
import {
  selectAllTasks,
  selectTaskLoading,
  selectTaskError,
  selectIsTasksEmpty,
  selectTasksCount
} from '../../store/task/task.selectors';
import { loadTasks, deleteTask } from '../../store/task/task.actions';
import { TaskStatusPipe } from '../../../shared/pipes/task-status.pipe';
import { selectAllTaskStatuses } from '../../store/task-status/task-status.selectors';
import { TaskStatus } from '../../models/task-status.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatTooltipModule,
    MatFormFieldModule,
    MatSelectModule,
    MatListModule,
    MatDividerModule,
    TaskStatusPipe
  ],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit, OnDestroy {

  sortField: string = 'createdAt';
  statusFilter: number | null = null;

  tasks$: Observable<Task[]>;
  loading$: Observable<boolean>;
  error$: Observable<any>;
  isEmpty$: Observable<boolean>;
  statuses$: Observable<TaskStatus[]>;

  filteredTasks$: Observable<Task[]>;

  private sortField$ = new BehaviorSubject<string>('createdAt');
  private statusFilter$ = new BehaviorSubject<number | null>(null);

  @Output() edit = new EventEmitter<Task>();

  private destroy$ = new Subject<void>();

  constructor(private store: Store, private snackBar: MatSnackBar) {
    this.tasks$ = this.store.select(selectAllTasks);
    this.loading$ = this.store.select(selectTaskLoading);
    this.error$ = this.store.select(selectTaskError);
    this.isEmpty$ = this.store.select(selectIsTasksEmpty);
    this.statuses$ = this.store.select(selectAllTaskStatuses);


    this.filteredTasks$ =  combineLatest([
      this.tasks$,
      this.sortField$,
      this.statusFilter$
    ]).pipe(
      map(([tasks, sortField, statusFilter]) =>
        this.applyFilters(tasks, sortField, statusFilter)
      )
    );
  }

  ngOnInit() {
    this.store.dispatch(loadTasks());

    this.error$.pipe(takeUntil(this.destroy$)).subscribe(error => {
      if (error) {
        this.snackBar.open('Failed to load tasks. Please try again.', 'Close', {
          duration: 5000,
          panelClass: ['error-snackbar']
        });
      }
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private applyFilters(tasks: Task[], sortField: string, statusFilter: number | null): Task[] {
  let filtered = tasks ?? [];

  if (statusFilter !== null) {
    filtered = filtered.filter(t => t.statusId === statusFilter);
  }

  return [...filtered].sort((a, b) => {
    if (sortField === 'title') {
      return a.title?.localeCompare(b.title ?? '') ?? 0;
    }

    if (sortField === 'statusId') {
      return (a.statusId ?? 0) - (b.statusId ?? 0);
    }

    if (sortField === 'createdAt') {
      const aTime = new Date(a.createdAt ?? 0).getTime();
      const bTime = new Date(b.createdAt ?? 0).getTime();
      return bTime - aTime;
    }

    return 0;
  });
}


  onSortChange(field: string) {
    this.sortField = field;
    this.sortField$.next(field); 
  }

  onFilterChange(statusId: number | null) {
    this.statusFilter = statusId;
    this.statusFilter$.next(statusId);
  }

  refreshTasks() {
    this.store.dispatch(loadTasks());
  }

  editTask(task: Task) {
    this.edit.emit(task);
  }

  deleteTask(taskId: number) {
    if (confirm('Are you sure you want to delete this task?')) {
      this.store.dispatch(deleteTask({ taskId }));
      this.snackBar.open('Task deleted successfully', 'Close', {
        duration: 3000,
        panelClass: ['success-snackbar']
      });
    }
  }

  trackByTaskId(index: number, task: Task): number {
    return task.id;
  }
}
