import { Component, Input, OnChanges, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { Task } from '../../models/task.model';
import { TaskStatus } from '../../models/task-status.model';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectAllTaskStatuses } from '../../store/task-status/task-status.selectors';
import { addTask, updateTask } from '../../store/task/task.actions';
import { UpdateTasks } from '../../models/update-tasks.model';
import { CreateTasks } from '../../models/create-tasks.model';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss']
})
export class TaskFormComponent implements OnChanges {
  @Input() task: Task | null = null;
  @Output() taskSaved = new EventEmitter<void>();
  @Output() formReset = new EventEmitter<void>();

  form: FormGroup;
  statuses$: Observable<TaskStatus[]>;

  constructor(private fb: FormBuilder, private store: Store) {
    this.form = this.fb.group({
      id: [null],
      title: ['', Validators.required],
      description: [''],
      statusId: [null, Validators.required]
    });

    this.statuses$ = this.store.select(selectAllTaskStatuses);

    this.form.disable();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.task) {
      this.form.patchValue(this.task);
      this.form.enable();
    }
  }

  save() {
    if (this.form.valid) {
      if (this.form.value.id) {
        const updateTaskData: UpdateTasks = {
          ...this.form.value,
        };

        this.store.dispatch(updateTask({ task: updateTaskData }));
      } else {
        const newTask: CreateTasks = {
          ...this.form.value,
        };

        this.store.dispatch(addTask({ task: newTask }));
      }

      this.form.reset();
      this.form.markAsPristine();
      this.form.markAsUntouched();
      this.form.disable();
      this.taskSaved.emit();
    }
  }

  add() {   this.form.enable();  }

  cancel() {
    this.form.reset();
    this.form.disable();
    this.formReset.emit();
  }
}
