import { Injectable } from '@angular/core';
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Injectable({
  providedIn: 'root'
})
export class SweetAlertService {

  constructor() { }

  showSuccess(title, message) {
    Swal.fire(title, message, 'success')
  }

  showError(error, title) {
    Swal.fire({
      icon: 'error',
      title: title,
      text: error
    })
  }
}
