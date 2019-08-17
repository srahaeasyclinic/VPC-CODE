import { Validator } from './validator'
import { Operation } from './operation';

export class FieldModel {
  name: string;
  DataType: string;
  controlType: string;
  PickListId: string;
  ReadOnly: boolean;
  Validators: Validator[];
  Fields: FieldModel[];
  DecimalPrecision: number;
  IsApplicableForFilter: boolean;
  refId: string;
  defaultValue: string;
  properties: string;
  isQueryable: boolean;
  typeOf: string;
  dataType: string;
  readOnly: boolean;
  //isRowSelected: boolean;
  //sequence: number;
  draggedItem: boolean;
  accessibleLayoutTypes: number[];
  toolbar: Operation[];
  entityName: string;
  applicableForSimpleSearch: boolean;
  applicableForAdvanceSearch: boolean;
  applicableForFreeTextSearch: boolean;
  required: boolean;
  isTagable: boolean;
  supportedQuickAddModes:number[];
  isQuickAddSupported:boolean
}
