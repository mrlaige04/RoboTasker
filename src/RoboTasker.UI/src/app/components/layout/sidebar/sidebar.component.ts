import {Component, inject, OnInit} from '@angular/core';
import {LayoutService} from '../../../services/layout/layout.service';
import {Divider} from 'primeng/divider';
import {Button} from 'primeng/button';
import {AuthService} from '../../../services/auth/auth.service';
import {BaseComponent} from '../../common/base/base.component';
import {MenuItem} from 'primeng/api';
import {Menu} from 'primeng/menu';
import {Ripple} from 'primeng/ripple';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {PermissionsNames} from '../../../models/tenants/permissions/permissions-names';
import {HasPermissionDirective} from '../../../utils/directives/has-permission.directive';
import {JsonPipe} from '@angular/common';
import {CurrentUserService} from '../../../services/user/current-user.service';

@Component({
  selector: 'rb-sidebar',
  imports: [
    Divider,
    Button,
    Menu,
    Ripple,
    RouterLink,
    RouterLinkActive,
    HasPermissionDirective,
    JsonPipe
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent extends BaseComponent implements OnInit {
  private layoutService = inject(LayoutService);
  private authService = inject(AuthService);
  private currentUser = inject(CurrentUserService);

  public sidebarOpened = this.layoutService.sidebarOpened;
  public isDesktop = this.layoutService.isDesktop;

  constructor() {
    super();
  }

  menu: RbMenuItem[] = [
    {
      label: 'Robots',
      items: [
        {
          label: 'Categories',
          routerLink: 'categories',
          icon: 'pi pi-bars',
          permission: PermissionsNames.CategoriesRead
        },
        {
          label: 'Capabilities',
          routerLink: 'capabilities',
          icon: 'pi pi-bolt',
          permission: PermissionsNames.CapabilitiesRead
        },
        {
          label: 'Robots',
          routerLink: 'robots',
          icon: 'pi pi-android',
          permission: PermissionsNames.RobotsRead
        },
        {
          label: 'Tasks',
          routerLink: 'tasks',
          icon: 'pi pi-check-square',
          permission: PermissionsNames.TasksRead
        }
      ]
    },
    {
      label: 'Users Management',
      items: [
        {
          label: 'Users',
          routerLink: 'tenant/users',
          icon: 'pi pi-users',
          permission: PermissionsNames.UsersRead
        },
        {
          label: 'Roles',
          routerLink: 'tenant/roles',
          icon: 'pi pi-crown',
          permission: PermissionsNames.RolesRead
        },
        {
          label: 'Permissions',
          routerLink: 'tenant/permissions',
          icon: 'pi pi-key',
          permission: PermissionsNames.PermissionsRead
        }
      ]
    },
    {
      label: 'Settings',
      items: [
        {
          label: 'Settings',
          routerLink: 'user/settings',
          icon: 'pi pi-cog',
          permission: PermissionsNames.TenantSettingsRead
        }
      ]
    }
  ];

  filteredMenu: RbMenuItem[] = [];

  ngOnInit() {
    this.filteredMenu = this.filterMenuByPermissions(this.menu);
  }

  closeSidebar() {
    this.layoutService.closeSidebar();
  }

  onNavigate(link: string) {
    if (!this.layoutService.isDesktop) {
      this.layoutService.closeSidebar();
    }

    this.router.navigateByUrl(link);
  }

  filterMenuByPermissions(menu: RbMenuItem[]): RbMenuItem[] {
    return menu
      .map(category => ({
        ...category,
        items: category.items?.filter(item => this.hasPermission(item.permission)) ?? []
      }))
      .filter(category => category.items.length > 0);
  }

  override hasPermission(permission?: string) {
    if (!permission) return true;
    const user = this.currentUser.currentUser();
    return user?.permissions.some(p => p.name === permission) ?? false;
  }

  logout() {
    this.authService.logout();
  }
}

export interface RbMenuItem extends MenuItem {
  permission?: string;
  items?: RbMenuItem[];
}
