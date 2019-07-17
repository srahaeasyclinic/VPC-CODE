export class MenuItem {
  group: string;
  name: string;
  menuType: string;
  referenceEntity: string;
  path: string;
  sequence:string;
  }

  export class NewMenuItem { 
      tenantId:string;
      id:string;
      name: string;
      groupId:string;
      groupName:string;
      menuTypeId:number;
      menuTypeName:string;
      referenceEntityId:string;
      actionTypeId:number;
      actionTypeName:string;
      wellKnownLink:string;
      modifiedBy:string;
      layoutId:string;
      modifiedByName:string;
      modifiedDate:string; 
    }