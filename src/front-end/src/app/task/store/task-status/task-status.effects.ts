import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as TaskStatusActions from './task-status.actions';
import { catchError, map, mergeMap, of } from 'rxjs';
import { TaskApiService } from '../../../core/services/task-api.service';

@Injectable()
export class TaskStatusEffects {
  loadStatuses$;

  constructor(
    private actions$: Actions,
    private api: TaskApiService
  ) {
    this.loadStatuses$ = createEffect(() =>
      this.actions$.pipe(
        ofType(TaskStatusActions.loadTaskStatuses),
        mergeMap(() =>
          this.api.getTaskStatuses().pipe(
            map(statuses =>
              TaskStatusActions.loadTaskStatusesSuccess({ statuses })
            ),
            catchError(error =>
              of(TaskStatusActions.loadTaskStatusesFailure({ error }))
            )
          )
        )
      )
    );
  }
}
