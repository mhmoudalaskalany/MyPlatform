import { ErrorHandler } from '@angular/core';

export class AppGlobalErrorhandler implements ErrorHandler {
  handleError(error) {
    console.error(error);
  }
}
