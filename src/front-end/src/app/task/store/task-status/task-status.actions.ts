import { createAction, props } from '@ngrx/store';
import { TaskStatus } from '../../models/task-status.model';

export const loadTaskStatuses = createAction('[Task Status] Load');
export const loadTaskStatusesSuccess = createAction(
  '[Task Status] Load Success',
  props<{ statuses: TaskStatus[] }>()
);
export const loadTaskStatusesFailure = createAction(
  '[Task Status] Load Failure',
  props<{ error: any }>()
);
