import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor(private toastr: ToastrService) { }

   showSuccess(message) {
    this.toastr.success(message);
  }

  showError(error) {
    this.toastr.error(error);
  }
}
