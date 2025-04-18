import {Component, DestroyRef, inject, OnInit} from '@angular/core';
import {BaseTableListComponent} from '../../../common/base-table-list/base-table-list.component';
import {RoleBase} from '../../../../models/tenants/roles/role-base';
import {RolesService} from '../../../../services/tenants/roles.service';
import {Guid} from 'guid-typescript';
import {finalize, tap} from 'rxjs';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {TableComponent} from '../../../common/table/table.component';
import {Button} from 'primeng/button';
import {HasPermissionDirective} from '../../../../utils/directives/has-permission.directive';
import {PermissionsNames} from '../../../../models/tenants/permissions/permissions-names';

@Component({
  selector: 'nb-roles-list',
  imports: [
    TableComponent,
    Button,
    HasPermissionDirective
  ],
  templateUrl: './roles-list.component.html',
  styleUrl: './roles-list.component.scss'
})
export class RolesListComponent extends BaseTableListComponent<RoleBase> implements OnInit {
  private rolesService = inject(RolesService);
  override destroyRef = inject(DestroyRef);

  constructor() {
    super();
    this.columns = [
      { label: 'Name', propName: 'name' },
      { label: 'System', propName: (role) => role.isSystem ? 'Yes' : 'No',  },
    ];
  }

  ngOnInit() {
    this.getData();
  }

  openAddNew() {
    this.router.navigate(['tenant', 'roles', 'add']);
  }

  openEditRole(id: Guid) {
    this.router.navigate(['tenant', 'roles', id]);
  }

  override getData() {
    this.isLoading.set(true);
    this.rolesService.getRoles({
      pageNumber: this.pageNumber(),
      pageSize: this.pageSize()
    }).pipe(
      tap((result) => {
        this.items.set(result);
      }),
      takeUntilDestroyed(this.destroyRef),
      finalize(() => {
        this.isLoading.set(false);
      })
    ).subscribe();
  }

  override deleteItem(id: Guid) {
    return this.rolesService.deleteRole(id);
  }

  protected readonly PermissionsNames = PermissionsNames;
}
