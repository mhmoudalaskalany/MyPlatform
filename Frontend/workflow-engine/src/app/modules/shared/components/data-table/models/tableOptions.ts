import { ColumnsInterface } from './columns.interface';
import { ActionsInterface } from './actions.interface';
import { URL } from './url';
import { SortModel } from './SortModel';
export class TableOptions {
  inputCols?: ColumnsInterface[] = [];
  inputActions?: ActionsInterface[] = [];
  inputUrl?: URL = {};
  inputPermissions?: string[] = [];
  inputName?: string;
  appId ?= 0;
  includeAppId ?= false;
  filter?: any;
  route?: string;
  orderByValue?: SortModel[] = [];
}
