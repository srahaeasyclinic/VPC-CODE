export class LayoutBasicModel {
  id: string;
  EntityId: string;
  EntityName: string;
  name: string; 
  pluralName: string; 
  singularName: string;
  layoutType: number;
  layoutTypeName: string;
  Subtype: number;
  subtypeeName: string;
  Context: number;
  contextName: string;

  //CreatedBy: number;
  //CreatedByName: string;
  //CreatedDate: Date;
  ModifiedBy: string;
  ModifiedByName: string;
  modifiedDate: Date;
  defaultLayout: boolean;
  ShowDefault: string;
}
