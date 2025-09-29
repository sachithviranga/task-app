import { createReducer, on } from '@ngrx/store';
import * as TaskActions from './task.actions';
import { Task } from '../../models/task.model';

export interface TaskState {
  tasks: Task[];
  loading: boolean;
  error: any;
}

export const initialState: TaskState = {
  tasks: [],
  loading: false,
  error: null
};

export const taskReducer = createReducer(
  initialState,

  on(TaskActions.loadTasks, state => ({
    ...state,
    loading: true
  })),

  on(TaskActions.loadTasksSuccess, (state, { tasks }) => ({
    ...state,
    tasks,
    loading: false
  })),

  on(TaskActions.loadTasksFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false
  })),

  on(TaskActions.addTask, (state, { task }) => ({
    ...state,
  })),

  on(TaskActions.updateTask, (state, { task }) => ({
    ...state,
  })),

  on(TaskActions.deleteTask, (state, { taskId }) => ({
    ...state,
    tasks: state.tasks.filter(t => t.id !== taskId)
  }))
);
