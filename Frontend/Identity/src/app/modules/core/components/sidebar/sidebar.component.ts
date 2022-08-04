import { Component, OnInit } from '@angular/core';
import { HttpService } from '../../services/http/http.service';
import { SidebarToggleService } from '../../services/sidebar-toggle/sidebar-toggle.service';

interface SidebarDefault {
  label: string;
  routerLinkName: string;
  icon?: string;
  svgIcon?: string;
  permissions?: string[],
}
interface Sidebar extends SidebarDefault {
  children?: SidebarDefault[];
}

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  user: any;
  isSidebarOpened = false;
  sidebarItems: Sidebar[] = [
    {
      label: 'Pages.Dashboard.Title',
      icon: 'bx bxs-dashboard',
      routerLinkName: '/dashboard',
      permissions: [],
    },
    {
      label: 'Pages.Lookups.Title',
      icon: 'bx bx-chart',
      routerLinkName: '/lookups',
      permissions: [],
      children: [
        {
          label: 'Pages.Lookups.Actions.Title',
          icon: 'bx bx-food-menu',
          routerLinkName: '/lookups/actions',
          permissions: [],
        }
      ]
    }
  ];

  constructor(private sidebarService: SidebarToggleService) { }

  ngOnInit(): void {
    this.sidebarService.isSidebarOpened$.subscribe(isSidebarOpened => this.isSidebarOpened = isSidebarOpened);
  }

  /**
   * Toggle Side Bar
   */
  sidebarToggle() {
    this.sidebarService.toggle();
  }
}
