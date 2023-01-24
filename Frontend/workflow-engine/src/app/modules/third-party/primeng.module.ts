import { NgModule } from '@angular/core';
import { TableModule } from 'primeng/table';
import { CheckboxModule } from 'primeng/checkbox';
import { FileUploadModule } from 'primeng/fileupload';
import { VirtualScrollerModule } from 'primeng/virtualscroller';

@NgModule({
    declarations: [],
    imports: [
        TableModule,
        CheckboxModule,
        FileUploadModule,
        VirtualScrollerModule
    ],
    exports: [
        TableModule,
        CheckboxModule,
        FileUploadModule,
        VirtualScrollerModule
    ]
})
export class PrimeNgModule { }
