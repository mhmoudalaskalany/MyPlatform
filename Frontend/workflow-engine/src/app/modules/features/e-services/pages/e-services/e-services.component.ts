import { Component, OnInit } from '@angular/core';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { Router } from '@angular/router';
import { LookupsService } from 'shared/services/lookups/lookups.service';
import { EServicesService } from '../../services/e-services.service';

@Component({
  selector: 'app-e-services',
  templateUrl: './e-services.component.html',
  styleUrls: ['./e-services.component.scss']
})
export class EServicesComponent implements OnInit {


  services: any;
  filterApps: any;
  categories: any[] = [];
  categoryNameEn;
  categoryNameAr;
  searchValue: any;
  selectedOption: string;

  get Service(): EServicesService { return Shell.Injector.get(EServicesService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get LookupService(): LookupsService { return Shell.Injector.get(LookupsService); }
  constructor(private router: Router) { }

  ngOnInit(): void {
    this.Localize.currentLanguage$.subscribe(() => {
      this.getCategories();
      this.getService();
    });
  }

  getService() {
    this.Service.getAll().subscribe((res: any) => {
      this.filterApps = res.data;
      this.getSelectedOption(this.selectedOption);
    });
  }

  getCategories() {
    this.categories = [];
    this.LookupService.getCategories().subscribe((res: any) => {
      this.categories = res;
      this.categories.unshift({
        id: -1,
        nameEn: 'All',
        nameAr: 'الكل'
      });
    });
  }

  filter(searchInput) {
    if (searchInput.target.value) {
      this.searchValue = searchInput.target.value;

      let filteredValues;
      if (this.Localize.isEnglish()) {
        if (this.categoryNameEn) {
          if (this.services.length) {
            filteredValues = this.services.filter(service => service.nameEn.toLowerCase().includes(this.searchValue.toLowerCase()) && (service.category ? service.category.nameEn.toLowerCase() : '') === this.categoryNameEn.toLowerCase());
          } else {
            filteredValues = this.filterApps.filter(service => service.nameEn.toLowerCase().includes(this.searchValue.toLowerCase()) && (service.category ? service.category.nameEn.toLowerCase() : '') === this.categoryNameEn.toLowerCase());
          }
        } else {
          if (this.services.length) {
            filteredValues = this.services.filter(service => service.nameEn.toLowerCase().includes(this.searchValue.toLowerCase()));
          } else {
            filteredValues = this.filterApps.filter(service => service.nameEn.toLowerCase().includes(this.searchValue.toLowerCase()));
          }
        }
      } else {
        if (this.categoryNameAr) {
          filteredValues = this.services.filter(service => service.nameAr.toLowerCase().includes(this.searchValue.toLowerCase()) && (service.category ? service.category.nameAr.toLowerCase() : '') === this.categoryNameAr.toLowerCase());
        } else {
          filteredValues = this.services.filter(service => service.nameAr.toLowerCase().includes(this.searchValue.toLowerCase()));
        }
      }

      this.services = filteredValues;
    } else {
      this.services = this.filterApps;
    }
  }

  getSelectedOption(selectedOption) {
    if (selectedOption?.id == -1) {
      this.services = this.filterApps;
      return;
    }
    if (selectedOption) {
      this.selectedOption = selectedOption;
      this.categoryNameEn = selectedOption.nameEn;
      this.categoryNameAr = selectedOption.nameAr;

      let filteredValues,
        allServices = JSON.parse(JSON.stringify(this.filterApps));

      if (this.searchValue) {
        if (this.Localize.isEnglish()) {
          filteredValues = allServices.filter(service => service.nameEn.toLowerCase().includes(this.searchValue.toLowerCase()) && (service.category ? service.category.nameEn.toLowerCase() : '') === this.categoryNameEn.toLowerCase());
        } else {
          filteredValues = allServices.filter(service => service.nameAr.toLowerCase().includes(this.searchValue.toLowerCase()) && (service.category ? service.category.nameAr.toLowerCase() : '') === this.categoryNameAr.toLowerCase());
        }
      } else {
        if (this.Localize.isEnglish()) {
          filteredValues = allServices.filter(service => (service.category ? service.category.nameEn.toLowerCase() : '') === this.categoryNameEn.toLowerCase());
        } else {
          filteredValues = allServices.filter(service => (service.category ? service.category.nameAr.toLowerCase() : '') === this.categoryNameAr.toLowerCase());
        }
      }

      this.services = filteredValues;
    } else {
      this.services = this.filterApps;
    }
  }
}
