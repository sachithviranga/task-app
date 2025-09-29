import { createReducer, on } from '@ngrx/store';
import * as TaskStatusActions from './task-status.actions';
import { TaskStatus } from '../../models/task-status.model';

export interface TaskStatusState {
  statuses: TaskStatus[];
  loading: boolean;
  error: any;
}

export const initialState: TaskStatusState = {
  statuses: [],
  loading: false,
  error: null
};

export const taskStatusReducer = createReducer(
  initialState,

  on(TaskStatusActions.loadTaskStatuses, state => ({
    ...state,
    loading: true
  })),

  on(TaskStatusActions.loadTaskStatusesSuccess, (state, { statuses }) => ({
    ...state,
    statuses,
    loading: false
  })),

  on(TaskStatusActions.loadTaskStatusesFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false
  }))
);
