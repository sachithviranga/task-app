import { createFeatureSelector, createSelector } from '@ngrx/store';
import { TaskState } from './task.reducer';

export const selectTaskState = createFeatureSelector<TaskState>('tasks');

export const selectAllTasks = createSelector(
  selectTaskState,
  (state: TaskState) => state.tasks || []
);

export const selectTaskLoading = createSelector(
  selectTaskState,
  (state: TaskState) => state.loading
);

export const selectTaskError = createSelector(
  selectTaskState,
  (state: TaskState) => state.error
);

export const selectTasksCount = createSelector(
  selectAllTasks,
  (tasks) => tasks?.length || 0
);

export const selectHasTasks = createSelector(
  selectTasksCount,
  (count) => count > 0
);

export const selectIsTasksEmpty = createSelector(
  selectHasTasks,
  (hasTasks) => !hasTasks
);
