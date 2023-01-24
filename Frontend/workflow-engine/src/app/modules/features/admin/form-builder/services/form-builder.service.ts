import { Observable } from 'rxjs';
import { HttpService } from 'core/services/http/http.service';
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})

export class FormBuilderService extends HttpService {
    get baseUrl(): string {
        return 'Controls/'
    }



}