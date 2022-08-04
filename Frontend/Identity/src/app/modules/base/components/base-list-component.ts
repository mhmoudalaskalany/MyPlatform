
import { Directive, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from './base-component';




@Directive()
export abstract class BaseListComponent extends BaseComponent implements OnInit {

  constructor(activatedRoute: ActivatedRoute) {
    super(activatedRoute)
  }

  override ngOnInit(): void {
    super.ngOnInit();
  }
  Redirect() {
    const currentRoute = this.route.url;
    const index = currentRoute.lastIndexOf('/');
    const str = currentRoute.substring(0, index);
    this.route.navigate([str]);
  }


}
