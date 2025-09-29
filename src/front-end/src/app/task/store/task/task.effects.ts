import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as TaskActions from './task.actions';
import { catchError, map, mergeMap, of } from 'rxjs';
import { TaskApiService } from '../../../core/services/task-api.service';
import { Task } from '../../models/task.model';

@Injectable()
export class TaskEffects {
  loadTasks$;
  addTask$;
  updateTask$;
  deleteTask$;

  constructor(
    private actions$: Actions,
    private api: TaskApiService
  ) {
    // 🔹 Load Tasks
    this.loadTasks$ = createEffect(() =>
      this.actions$.pipe(
        ofType(TaskActions.loadTasks),
        mergeMap(() =>
          this.api.getTasks().pipe(
            map((tasks: Task[]) => TaskActions.loadTasksSuccess({ tasks })),
            catchError(error =>
              of(TaskActions.loadTasksFailure({ error }))
            )
          )
        )
      )
    );

    // 🔹 Add Task
    this.addTask$ = createEffect(() =>
      this.actions$.pipe(
        ofType(TaskActions.addTask),
        mergeMap(({ task }) =>
          this.api.addTask(task).pipe(
            // Reuse loadTasks after add, OR dispatch a specific success
            map((newTask: Task) =>
              TaskActions.loadTasks() // refresh list after add
            ),
            catchError(error =>
              of(TaskActions.loadTasksFailure({ error }))
            )
          )
        )
      )
    );

    // 🔹 Update Task
    this.updateTask$ = createEffect(() =>
      this.actions$.pipe(
        ofType(TaskActions.updateTask),
        mergeMap(({ task }) =>
          this.api.updateTask(task).pipe(
            map((updatedTask: Task) =>
              TaskActions.loadTasks() // refresh list after update
            ),
            catchError(error =>
              of(TaskActions.loadTasksFailure({ error }))
            )
          )
        )
      )
    );

    // 🔹 Delete Task
    this.deleteTask$ = createEffect(() =>
      this.actions$.pipe(
        ofType(TaskActions.deleteTask),
        mergeMap(({ taskId }) =>
          this.api.deleteTask(taskId).pipe(
            map(() =>
              TaskActions.loadTasks() // refresh list after delete
            ),
            catchError(error =>
              of(TaskActions.loadTasksFailure({ error }))
            )
          )
        )
      )
    );
  }
}
