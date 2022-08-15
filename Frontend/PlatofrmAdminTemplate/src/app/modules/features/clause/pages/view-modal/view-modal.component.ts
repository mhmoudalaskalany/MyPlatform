import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/translation/translation.service';

@Component({
  selector: 'app-view-modal',
  templateUrl: './view-modal.component.html',
  styleUrls: ['./view-modal.component.scss']
})
export class ViewModalComponent implements OnInit {

  fromPage: any;

  get Localize(): TranslationService {
    return Shell.Injector.get(TranslationService);
  }

  constructor(public dialogRef: MatDialogRef<ViewModalComponent>, @Optional() @Inject(MAT_DIALOG_DATA) public data: any) {
    this.fromPage = data.pageValue;
    console.log(this.fromPage);
    
  }

  ngOnInit(): void { }
}
