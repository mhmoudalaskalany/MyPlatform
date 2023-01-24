import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { EServicesService } from 'features/e-services/services/e-services.service';
import { FormBuilderService } from '../../services/form-builder.service';

@Component({
  selector: 'app-add-control',
  templateUrl: './add-control.component.html',
  styleUrls: ['./add-control.component.scss']
})
export class AddControlComponent implements OnInit {

  code: string;
  control: any;
  serviceId: any;
  services: any[] = [];
  controlTypes: any[] = [
    {
      id: 1,
      nameEn: 'Text',
      nameAr: 'نص',
      code: 'text'
    },
    {
      id: 2,
      nameEn: 'TextArea',
      nameAr: 'محرر نص',
      code: 'textarea'
    },
    {
      id: 3,
      nameEn: 'Dropdown',
      nameAr: 'قائمة منسدلة',
      code: 'dropdown'
    },
    {
      id: 4,
      nameEn: 'Checkbox',
      nameAr: 'صندوق اختيار',
      code: 'checkbox'
    },
    {
      id: 5,
      nameEn: 'DatePicker',
      nameAr: 'تاريخ',
      code: 'datepicker'
    },
    {
      id: 6,
      nameEn: 'Attachment',
      nameAr: 'مرفق',
      code: 'attachment'
    }
  ];
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get Service(): FormBuilderService { return Shell.Injector.get(FormBuilderService); }
  get EService(): EServicesService { return Shell.Injector.get(EServicesService); }
  constructor() { }

  ngOnInit(): void {
    this.getServices();
  }

  renderControlComponent(event): void {
    this.code = event.code;
  }

  getServices(): void {
    this.EService.getAll().subscribe((res: any) => {
      this.services = res.data;
    });
  }

  setServiceId(event): void {
    if (event) {
      this.serviceId = event.id;
    } else {
      this.serviceId = null;
      this.code = null;
    }
  }
}
