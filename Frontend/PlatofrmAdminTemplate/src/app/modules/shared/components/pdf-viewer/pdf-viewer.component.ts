import { Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-pdf-viewer',
  templateUrl: './pdf-viewer.component.html',
  styleUrls: ['./pdf-viewer.component.scss']
})
export class PdfViewerComponent implements OnInit {
  @ViewChild('pdfViewer', { static: false }) public pdfViewer;
  @Input() pdfSrc;
  constructor(public dialogRef: MatDialogRef<PdfViewerComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.pdfViewer.pdfSrc = this.data.url;
      this.pdfViewer.refresh();
    }, 1);
  }


  close() {
    this.dialogRef.close(false);
  }



}

