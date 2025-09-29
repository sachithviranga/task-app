
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Task } from '../../task/models/task.model'
import { TaskStatus } from '../../task/models/task-status.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateTasks } from '../../task/models/create-tasks.model';
import { UpdateTasks } from '../../task/models/update-tasks.model';

@Injectable({ providedIn: 'root' })
export class TaskApiService {
  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.baseUrl}/tasks`);
  }

  addTask(task: CreateTasks): Observable<Task> {
    return this.http.post<Task>(`${this.baseUrl}/tasks`, task);
  }

  updateTask(task: UpdateTasks): Observable<Task> {
    return this.http.put<Task>(`${this.baseUrl}/tasks/${task.id}`, task);
  }

  deleteTask(taskId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/tasks/${taskId}`);
  }

  getTaskStatuses(): Observable<TaskStatus[]> {
    return this.http.get<TaskStatus[]>(`${this.baseUrl}/status`);
  }
}
