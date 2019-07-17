import { LayoutBasicModel } from './layoutbasicmodel';
import { ListLayoutDetails } from './listlayoutdetails';
import { ViewLayoutDetails } from './viewLayoutDetails';
import { Operation } from './operation';
import { Tasks } from './tasks';
import { FormLayoutDetails } from './formLayoutDetails';

export class LayoutModel extends LayoutBasicModel {
  layout: string;
  listLayoutDetails: ListLayoutDetails;
  formLayoutDetails: FormLayoutDetails;
  viewLayoutDetails: ViewLayoutDetails;
  //formLayoutDetails: FormLayoutDetails;
  //operations: Operation[];
  //tasks: Tasks[];
}
