import { Component, Input, OnInit } from '@angular/core';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';

@Component({
    selector: 'app-favorite-service',
    templateUrl: './favorite-service.component.html',
    styleUrls: ['./favorite-service.component.scss'],
})
export class FavoriteServiceComponent implements OnInit {

    @Input() service: any;
    get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
    constructor() {
    }

    ngOnInit(): void {

    }

}
