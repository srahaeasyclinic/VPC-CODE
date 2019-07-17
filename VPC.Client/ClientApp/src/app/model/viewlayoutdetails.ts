import { SelectedItem } from './selecteditem';
import { OrderDetails } from './orderdetails';
import { RowLevelOperations } from './rowleveloperations';

export class ViewLayoutDetails {
  public fields: SelectedItem[];
  defaultSortOrder: OrderDetails;  
  actions: RowLevelOperations[];
}
