
import { Component, OnInit } from '@angular/core';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { SidebarToggleService } from 'core/services/sidebar-toggle/sidebar-toggle.service';
import { StorageService } from 'core/services/storage/storage.service';


interface sidebar {
  label: string;
  icon?: string;
  routerLinkName: string;
  permission?: string,
  children?: {
    label: string;
    icon?: string;
    routerLinkName: string;
  }[];
};

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  user: any = {};
  isSidebarOpened = false;
  sidebarItems: sidebar[] = [
    {
      label: 'Common.Home',
      icon: 'bx bx-home-smile',
      routerLinkName: '/main/home'
    },
    {
      label: 'Pages.EServices',
      icon: 'bx bx-briefcase',
      routerLinkName: '/main/e-service'
    },
    {
      label: 'Pages.MyRequests',
      icon: 'bx bx-receipt',
      routerLinkName: '/main/my-requests'
    },
    {
      label: 'Pages.MyTasks',
      icon: 'bx bx-task',
      routerLinkName: '/main/my-tasks'
    },
    {
      label: 'Common.ADMIN_MANAGEMENT',
      icon: 'bx bx-cog',
      routerLinkName: '/main/admin',
      children: [
        {
          label: 'User.Title',
          icon: 'bx bx-user',
          routerLinkName: '/main/admin/users',
        },
        {
          label: 'Pages.ReviewType',
          icon: 'bx bxl-telegram',
          routerLinkName: '/main/admin/lookup/review-type',
        },
        {
          label: 'Pages.Category',
          icon: 'bx bxs-category',
          routerLinkName: '/main/admin/lookup/category',
        },
        {
          label: 'Pages.Groups',
          icon: 'bx bx-group',
          routerLinkName: '/main/admin/lookup/group',
        },
        {
          label: 'Pages.Services',
          icon: 'bx bx-briefcase',
          routerLinkName: '/main/admin/service',
        },
        {
          label: 'Pages.Workflows',
          icon: 'bx bxs-direction-left',
          routerLinkName: '/main/admin/workflow-builder',
        },
      ]
    },
  ];

  get Storage(): StorageService { return Shell.Injector.get(StorageService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
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
