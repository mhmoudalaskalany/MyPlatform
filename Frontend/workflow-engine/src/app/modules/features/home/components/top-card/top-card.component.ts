import { Component, Input, OnInit, ViewChild } from '@angular/core';

@Component({
    selector: 'app-top-card',
    templateUrl: './top-card.component.html',
    styleUrls: ['./top-card.component.scss'],
})
export class TopCardComponent implements OnInit {

    @Input() title: string;
    @Input() count: number = 0;
    @Input() backgroundColor: string;
    @Input() iconColor: string;
    @Input() icon: String;
    @Input() status: string;
    @Input() iconSize: string;
    @Input() backgroundIcon: string;
    constructor() {
    }

    ngOnInit(): void {

    }

}
