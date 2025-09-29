import { createFeatureSelector, createSelector } from '@ngrx/store';
import { TaskStatusState } from './task-status.reducer';

export const selectTaskStatusState = createFeatureSelector<TaskStatusState>('taskStatuses');

export const selectAllTaskStatuses = createSelector(
  selectTaskStatusState,
  state => state.statuses
);
