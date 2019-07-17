import { SelectedItem } from './selecteditem';
import { OrderDetails } from './orderdetails';
import { SearchProperties } from './searchproperties';
import { RowLevelOperations } from './rowleveloperations';
import { Operation } from './operation';

export class ListLayoutDetails {
  public fields: SelectedItem[];
  defaultSortOrder: OrderDetails;
  maxResult: number;
  searchProperties: SearchProperties[];
  actions: RowLevelOperations[];
  toolbar: Operation[];
  defaultGroupBy : string;
}
