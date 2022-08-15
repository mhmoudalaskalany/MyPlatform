import { Directive, ElementRef, Renderer2 } from '@angular/core';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/translation/translation.service';
import { Subject, takeUntil } from 'rxjs';

@Directive({
  selector: '[setRtl]'
})
export class RtlDirective {

  private destroy$ = new Subject<any>();

  get localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  constructor(private elRef: ElementRef, private renderer: Renderer2) {
    this.switchClassBasedOnLanguage();
  }

  /*Switch rtl class based on the chosen language from Translation Service*/
  switchClassBasedOnLanguage() {
    this.localize.currentLanguage$.pipe(takeUntil(this.destroy$)).subscribe(language => this.handleBasicLogic(language));
  }

  private handleBasicLogic(language: string) {
    if (language === 'ar') {
      this.renderer.addClass(this.elRef.nativeElement, 'rtl');
      this.renderer.setAttribute(this.elRef.nativeElement, 'dir', 'rtl');
    } else {
      this.renderer.removeClass(this.elRef.nativeElement, 'rtl');
      this.renderer.setAttribute(this.elRef.nativeElement, 'dir', 'ltr');
    }
  }

  /* when leaving the component */
  ngOnDestroy() {
    //Called once, before the instance is destroyed.
    //Add 'implements OnDestroy' to the class.
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }
}
