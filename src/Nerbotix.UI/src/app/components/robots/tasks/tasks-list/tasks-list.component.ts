import {Component, DestroyRef, inject, OnInit} from '@angular/core';
import {BaseTableListComponent} from '../../../common/base-table-list/base-table-list.component';
import {Guid} from 'guid-typescript';
import {catchError, finalize, Observable, of, tap} from 'rxjs';
import {Success} from '../../../../models/success';
import {TasksService} from '../../../../services/robots/tasks.service';
import {TableComponent} from '../../../common/table/table.component';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {TaskStatus} from '../../../../models/robots/tasks/task-status';
import {Button} from 'primeng/button';
import {Tooltip} from 'primeng/tooltip';
import {RouterLink} from '@angular/router';
import {PermissionsNames} from '../../../../models/tenants/permissions/permissions-names';
import {HasPermissionDirective} from '../../../../utils/directives/has-permission.directive';
import {HttpErrorResponse} from '@angular/common/http';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'nb-tasks-list',
  imports: [
    TableComponent,
    Button,
    Tooltip,
    RouterLink,
    HasPermissionDirective,
    DatePipe
  ],
  templateUrl: './tasks-list.component.html',
  styleUrl: './tasks-list.component.scss'
})
export class TasksListComponent extends BaseTableListComponent<any> implements OnInit {
  private tasksService = inject(TasksService);
  override destroyRef = inject(DestroyRef);

  constructor() {
    super();
    this.columns = [
      { label: 'Name', propName: 'name' },
      { label: 'Description', propName: 'description' },
      { label: 'Status', propName: 'status' },
      { label: 'Completed', propName: 'completedAt' },
      { label: 'Estimate', propName: 'estimatedDuration' },
      { label: 'Priority', propName: 'priority', sortable: true },
      { label: 'Complexity', propName: 'complexity' },
      { label: 'Robot', propName: 'assignedRobotId' }
    ];
  }

  ngOnInit() {
    this.getData();
  }

  openAddNew() {
    this.router.navigate(['tasks', 'add']);
  }

  openEditTask(id: Guid) {
    this.router.navigate(['tasks', id]);
  }

  reEnqueue(id: Guid) {
    this.showLoader();
    this.tasksService.reEnqueue(id).pipe(
      catchError((error: HttpErrorResponse) => {
        const message = error.error?.detail;
        this.notificationService.showError('Error while enqueueing task', message);
        return of(null);
      }),
      tap((res) => {
        if (res) {
          this.notificationService.showSuccess('OK', 'Task was sent to enqueue');
          this.getData();
        }
      }),
      takeUntilDestroyed(this.destroyRef),
      finalize(() => {
        this.hideLoader();
      })
    ).subscribe();
  }

  override getData() {
    this.isLoading.set(true);
    this.tasksService.getTasks({
      pageNumber: this.pageNumber(),
      pageSize: this.pageSize()
    }).pipe(
      tap(result => {
        this.items.set(result);
      }),
      takeUntilDestroyed(this.destroyRef),
      finalize(() => {
        this.isLoading.set(false);
      })
    ).subscribe();
  }

  cancelTask(id: Guid) {
    this.confirmationService.confirm({
      header: 'Confirmation',
      icon: 'pi pi-info-circle',
      rejectLabel: 'Cancel',
      rejectButtonProps: {
        label: 'Cancel',
        outlined: true,
        severity: 'secondary'
      },
      acceptButtonProps: {
        label: 'Confirm',
        severity: 'danger'
      },
      message: 'Are you sure you want to cancel this task?',
      accept: () => {
        this.tasksService.cancelTask(id).pipe(
          tap(result => {
            if (result) {
              this.getData();
            }
          }),
          takeUntilDestroyed(this.destroyRef),
          finalize(() => {
            this.isLoading.set(false);
          })
        ).subscribe();
      }
    });
  }

  override deleteItem(id: Guid): Observable<Success> {
    return this.tasksService.deleteTask(id);
  }

  protected readonly TaskStatus = TaskStatus;
  protected readonly PermissionsNames = PermissionsNames;
}
