import { Validators } from '@angular/forms';

export interface ColumnsInterface {
  field: string;
  header: string;
  placeholder?: string;
  sort?: boolean;
  sortCol?: string;
  sortName?: string;
  filterMode?: string;
  filterColumnName?: string;
  dataType?: string;
  filter?: boolean;

}
