import { Component, Injector, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Shell } from 'base/components/shell';
import * as moment from 'moment';
import { Subject, takeUntil } from 'rxjs';
import { LoadingService } from './modules/core/services/loading/loading.service';
import { TranslationService } from './modules/core/services/translation/translation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  show = false;
  private destroy$ = new Subject<any>();

  constructor(
    public inj: Injector, private translateService: TranslationService,
    private titleService: Title,
    private loading: LoadingService) {
    Shell.Injector = inj;
  }

  ngOnInit(): void {
    moment.locale('en-US');
    this.setTitle();
  }

  ngAfterContentInit(): void {
    this.loading.isLoading.pipe(takeUntil(this.destroy$)).subscribe(isLoading => {
      setTimeout(() => {
        this.show = isLoading;
      });
    });
  }

  setTitle(): void {
    if (this.translateService.isEnglish) {
      this.titleService.setTitle('System Name');
    } else {
      this.titleService.setTitle('عنوان النظام');
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }
}
