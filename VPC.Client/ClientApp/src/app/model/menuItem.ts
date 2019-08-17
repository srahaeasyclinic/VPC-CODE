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
      groupId:string="";
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
      modifiedDate: string; 
      parentId: string="00000000-0000-0000-0000-000000000000";
      sortItem: number = -0;
      subGroup: NewMenuItem[] = [];
      menuIcon: string="";
      menucode: string="";
      isMenuGroup: boolean = false;
      groupIdSort: number=-0;
  }

// export class MenuGroup {
//   id: string;
//   parent: string;
//   name: string;
//   code: string;
//   displayOrder: string;
//   icon: string;
//  }