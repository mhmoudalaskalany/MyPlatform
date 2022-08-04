import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DonutComponent } from './components/donut/donut.component';
import { BarComponent } from './components/bar/bar.component';
import { PieComponent } from './components/pie/pie.component';
import { NgApexchartsModule } from 'ng-apexcharts';
import { GradientDonutComponent } from './components/gradient-donut/gradient-donut.component';
import { TimelineComponent } from './components/timeline/timeline.component';
import { PolarComponent } from './components/polar/polar.component';
import { ProgressComponent } from './components/progress/progress.component';

@NgModule({
  declarations: [
    DonutComponent,
    BarComponent,
    PieComponent,
    GradientDonutComponent,
    TimelineComponent,
    PolarComponent,
    ProgressComponent
  ],
  imports: [
    NgApexchartsModule,
    CommonModule,

  ],
  exports: [
    NgApexchartsModule,
    DonutComponent,
    BarComponent,
    PieComponent,
    GradientDonutComponent,
    TimelineComponent,
    PolarComponent,
    ProgressComponent
  ]
})
export class ChartsModule { }
