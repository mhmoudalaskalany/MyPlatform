import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PageHeader } from 'shared/models/page-header';




@Component({
    selector: 'app-shared-page-header',
    templateUrl: './shared-page-header.component.html',
    styleUrls: ['./shared-page-header.component.scss']
})
export class SharedPageHeaderComponent implements OnInit {



    /** inputs */
    @Input() pageTitle: string;
    @Input() pageHeaderButtons: PageHeader;
    /**Outputs */



    constructor() { }

    ngOnInit() {

    }

}
