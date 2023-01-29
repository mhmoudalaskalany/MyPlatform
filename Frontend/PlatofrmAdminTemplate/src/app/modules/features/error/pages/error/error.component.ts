import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss']
})
export class ErrorComponent implements OnInit {

  errorNumber = '401';

  constructor(private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activeRoute.params.subscribe(data => {
      this.errorNumber = data['errorNumber'];
    });
  }

  logout() {
    localStorage.clear();
  }
}
