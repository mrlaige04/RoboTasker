import {Component, OnInit, signal} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {TabsModule} from 'primeng/tabs';
import {TabMenu} from 'primeng/tabmenu';
import {MenuItem} from 'primeng/api';
import {BaseComponent} from '../../common/base/base.component';

@Component({
  selector: 'rb-user-wrapper',
  imports: [
    RouterOutlet,
    TabsModule,
    TabMenu,
    RouterLink
  ],
  templateUrl: './user-wrapper.component.html',
  styleUrl: './user-wrapper.component.scss'
})
export class UserWrapperComponent extends BaseComponent implements OnInit {
  routes: MenuItem[] = [
    {
      label: 'Profile',
      icon: 'pi pi-user',
      routerLink: 'profile'
    },
    {
      label: 'Change password',
      icon: 'pi pi-lock',
      routerLink: 'change-password'
    },
    {
      label: 'Settings',
      icon: 'pi pi-cog',
      routerLink: 'settings'
    }
  ];

  activeMenu = signal<MenuItem>(this.routes[0]);

  ngOnInit() {
    this.activatedRoute.firstChild?.url.subscribe(urlSegments => {
      if (urlSegments.length > 0) {
        const menu = this.getMenuByUrl(urlSegments[0].path);
        if (menu) {
          this.activeMenu.set(menu);
        }
      }
    });
  }

  private getMenuByUrl(url: string) {
    return this.routes.find(m => m.routerLink === url);
  }

  onTabSelect(event: unknown) {
    const menu = this.getMenuByUrl(event as string);
    if (menu) {
      this.activeMenu.set(menu);
    }
  }
}
