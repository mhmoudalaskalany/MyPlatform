import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { RequestService } from '../../services/my-requests.service';
import { AlertService } from 'shared/services/alert/alert.service';
import { FileService } from 'shared/services/file-manager/file/file.service';

@Component({
  selector: 'app-request-details',
  templateUrl: './request-details.component.html',
  styleUrls: ['./request-details.component.scss']
})
export class RequestDetailsComponent implements OnInit {

  requestId: string;
  requestData;
  requestDetails;
  donationAmount

  get Alert(): AlertService { return Shell.Injector.get(AlertService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get RequestService(): RequestService { return Shell.Injector.get(RequestService); }
  get Router(): Router { return Shell.Injector.get(Router); }
  get File(): FileService { return Shell.Injector.get(FileService); }
  constructor(private activeRoute: ActivatedRoute, private _location: Location) { }

  ngOnInit(): void {
    this.requestId = this.activeRoute.snapshot.paramMap.get('id');

    this.RequestService.getRequestById(this.requestId).subscribe(requestData => {
      if (requestData) {
        this.requestData = requestData;


      } else {
        this.Router.navigate(['/main/my-requests']);
      }
    });
  }

  onCancel() {
    this._location.back();
  }

  download(link, name) {
    this.File.download(link, name);
  }
}
