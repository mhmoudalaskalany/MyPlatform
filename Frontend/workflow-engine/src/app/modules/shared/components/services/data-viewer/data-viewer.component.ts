import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-data-viewer',
  templateUrl: './data-viewer.component.html',
  styleUrls: ['./data-viewer.component.scss']
})
export class DataViewerComponent implements OnInit {

  @Input() data: any;
  requestDetailsKeys: string[];
  requestDetailsKeysWithArrayValue: string[];

  constructor() { }

  ngOnInit(): void {
    const requestDetailsKeys = Object.keys(this.data);
    this.requestDetailsKeys = JSON.parse(JSON.stringify(requestDetailsKeys));
    this.requestDetailsKeys = this.requestDetailsKeys.filter(key => !this.isValueTypeArray(this.data[key].value));
    
    this.requestDetailsKeysWithArrayValue = requestDetailsKeys.filter(key => this.isValueTypeArray(this.data[key].value));
  }

  isValueTypeArray(value): boolean {
    return Array.isArray(value) || (typeof value === 'object' && value !== null);
  }

  getKeysFromObject(object) {
    return Object.keys(object);
  }
}
