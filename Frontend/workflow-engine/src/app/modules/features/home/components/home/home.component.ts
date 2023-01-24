import { LoadOptions } from './../../../../shared/components/data-table/models/LoadOpts';
import { Service } from './../../../admin/service/models/service';
import { Shell } from './../../../../base/components/shell';
import { RequestService } from './../../../my-requests/services/my-requests.service';
import { debounceTime, distinctUntilChanged, take } from 'rxjs/operators';
import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslationService } from 'core/services/localization/translation.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  constructor() {
  }
  get request(): RequestService { return Shell.Injector.get(RequestService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }

  
  ngOnInit(): void {
    this.getallrequests();
    //this.countrequests();
  }

  requestlist:any[];
  model:any=[];
  count:number = 0;
  scrollSize = 10;
  scrollPageNumber = 1;
  photoUrl: string = '';
  getRequestPaginationOptions: LoadOptions = {
    pageSize: this.scrollSize,
    pageNumber: this.scrollPageNumber,
    searchCriteria: '',
    filter: {
      searchCriteria: '',
    },
  };

 getallrequests():void {
  this.request.getRequestCards()
  .pipe().subscribe((x: any) => {
    if (x) {
     this.model = x.data;
     console.log(x.data);
    // this.getPagedRequests();
    }
  });
 }

//  countrequests(): void{
//   this.request.getAllRequestsCount()
//   .pipe(debounceTime(500), distinctUntilChanged())
//   .subscribe((x: any) => {
//     if (x) {
//       this.model=x.data;
//       console.log(this.model);
//     }
//   })}

// }

countrequests(): void{
  this.request.getAllRequestsCount()
  .subscribe((x: any) => {
      this.model=x.data;
      console.log(this.model);
  })}

}
