import { Component, Input, OnInit } from '@angular/core';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { FileService } from 'shared/services/file-manager/file/file.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {

  requestDetails;

  @Input() details;

  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get File(): FileService { return Shell.Injector.get(FileService); }
  constructor() { }

  ngOnInit(): void {
    this.generateData(this.details);
  }

  /**
   * Generate Task Data
   * @param requestData 
   */
  generateData(requestData) {
    if (requestData) {
      const requestDetails = JSON.parse(requestData.requestBody);
      this.requestDetails = requestDetails;
    }
  }

  /**
   * Return Background Color For Action
   * @param color 
   * @returns 
   */
  setBackgroundColor(color): string {
    switch (color) {
      case 'Red': {
        return 'bg-error text-white';
      }
      case 'Orange': {
        return 'bg-orange text-success';
      }
      case 'Green': {
        return 'bg-success text-white ';
      }
      case 'Dark': {
        return 'bg-purple';
      }
      case 'Blue': {
        return 'bg-bluelight';
      }

      default: {
        return '';
      }
    }
  }

  download(link, name) {
    this.File.download(link, name);
  }
}
