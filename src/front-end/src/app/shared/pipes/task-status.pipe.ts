import { Pipe, PipeTransform } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectAllTaskStatuses } from '../../task/store/task-status/task-status.selectors';
import { inject, signal } from '@angular/core';

@Pipe({
  name: 'taskStatus',
  standalone: true,
  pure: false
})
export class TaskStatusPipe implements PipeTransform {
  private statuses = signal<{ [id: number]: string }>({});
  private store = inject(Store);

  constructor() {
    this.store.select(selectAllTaskStatuses).subscribe(statuses => {
      const map = [];
      for (let status of statuses) {
        map[status.id] = status.name;
      }
      this.statuses.set(map);
    });
  }

  transform(statusId: number): string {
    return this.statuses()[statusId] ?? 'Unknown';
  }
}
