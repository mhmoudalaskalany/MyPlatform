import { Component, Input, OnInit } from '@angular/core';
import { TranslationService } from 'core/services/localization/translation.service';

@Component({
  selector: 'app-service-card',
  templateUrl: './service-card.component.html',
  styleUrls: ['./service-card.component.scss']
})
export class ServiceCardComponent implements OnInit {

  @Input() service!: any;
  
  constructor(public translation: TranslationService) { }

  ngOnInit(): void { }
}
