import { createAction, props } from '@ngrx/store';
import { Task } from '../../models/task.model';
import { UpdateTasks } from '../../models/update-tasks.model';
import { CreateTasks } from '../../models/create-tasks.model';

export const loadTasks = createAction('[Tasks] Load Tasks');
export const loadTasksSuccess = createAction('[Tasks] Load Success', props<{ tasks: Task[] }>());
export const loadTasksFailure = createAction('[Tasks] Load Failure', props<{ error: any }>());

export const addTask = createAction('[Tasks] Add Task', props<{ task: CreateTasks }>());
export const updateTask = createAction('[Tasks] Update Task', props<{ task: UpdateTasks }>());
export const deleteTask = createAction('[Tasks] Delete Task', props<{ taskId: number }>());
