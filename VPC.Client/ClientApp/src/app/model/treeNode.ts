import { Operation } from '../model/operation';

export class Setting{
	showHeader:boolean;
	columnWidth:Number;
}

export class Validator{
	name:string;
	customizable: boolean;
	dblength?: number;
	userSetlength?: number;
	EmailFromat: string;
	value:boolean;
}
////////////////Interface of Treenode///////////////////////////////////////////////
export interface ITreeNode {
    name: string;
    value: any;
    required: boolean;
   // PickListId: string;
    fields: ITreeNode[];
    dataType: string;
    typeOf: string;
    controlType: string;
    readOnly: boolean;
    decimalPrecision: number;
    defaultValue: string;
    properties: string;
    tabs: ITreeNode[];
    setting: Setting;
    validators: Validator[];
    selectedView: any;
    receivingTypes: string[];
    receiverDataTypes: string[];
    broadcastingTypes: string[];
    refId: string;
    //draggedItem: boolean;
   // isQueryable: boolean;
    //IsApplicableForFilter: boolean;
    accessibleLayoutTypes: number[];
    toolbar: Operation[];
    entityName: string;
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////// Implement interface to class////////////////////////////////////////////////////////////
class TreeNode implements ITreeNode
{
    name: string;
    value: any;
    required: boolean;
    PickListId: string;
    fields: ITreeNode[];
    dataType: string;
    typeOf: string;
    controlType: string;
    readOnly: boolean;
    decimalPrecision: number;
    defaultValue: string;
    properties: string;
    tabs: ITreeNode[];
    setting: Setting;
    validators: Validator[];
    selectedView: any;
    receivingTypes: string[];
    receiverDataTypes: string[];
    broadcastingTypes: string[];
    refId: string;
   // draggedItem: boolean;
   // isQueryable: boolean;
   // IsApplicableForFilter: boolean;
    accessibleLayoutTypes: number[];
    toolbar: Operation[];
    entityName: string; 
    constructor(
        name: string="",
        value: any="",
        required: boolean=false,    
        fields: TreeNode[]=[],
        dataType: string="",
        typeOf: string="",
        controlType: string="",
        readOnly: boolean=false,
        decimalPrecision?: number,    
        defaultValue: string="",    
        properties: string="",    
        tabs: TreeNode[]=[],    
        setting?: Setting,    
        validators?: Validator[],     
        selectedView?: any,    
        receivingTypes: string[]=[],    
        receiverDataTypes: string[]=[],    
        broadcastingTypes: string[]=[],    
        refId: string="",    
        //IsApplicableForFilter?: boolean,
       // draggedItem?: boolean,
       // isQueryable?: boolean,
       // PickListId?:string,
        accessibleLayoutTypes: number[]=[],    
        toolbar: Operation[]=[],    
        entityName: string="",
    )
    {
        this.name = name;
        this.value=value;
        this.required = required;   
        this.fields=fields;
        this.dataType = dataType;
        this.typeOf = typeOf;
        this.controlType = controlType;
        this.readOnly = readOnly;
        this.decimalPrecision = decimalPrecision;   
        this.defaultValue = defaultValue;    
        this.properties = properties;   
        this.tabs = tabs;   
        this.setting = setting;   
        this.validators = validators;     
        this.selectedView = selectedView;    
        this.receivingTypes = receivingTypes;   
        this.receiverDataTypes = receiverDataTypes; 
        this.broadcastingTypes = broadcastingTypes;    
        this.refId = refId;   
        this.accessibleLayoutTypes = accessibleLayoutTypes; 
        this.toolbar = toolbar;  
        this.entityName = entityName;
        
       // this.IsApplicableForFilter = IsApplicableForFilter;
       // this.draggedItem = draggedItem;
        //this.isQueryable = isQueryable;
        //this.PickListId = PickListId;
    }
    
}
/////////////////////////////////////// Instanciate for treeNode///////////////////////////////////////////////////////////////////
export function getTreenodeinstanceWithObject(item?:any)
{
   // console.log('getTreenodeinstanceWithObject '+JSON.stringify(item));
    if (item === null)
    {
        return new TreeNode();
    }
    return new TreeNode(item.name,
        item.value,
        item.required,
        item.fields,
        item.dataType,
        item.typeOf,
        item.controlType,
        item.readOnly,
        item.decimalPrecision,
        item.defaultValue,
        item.properties,
        item.tabs,
        item.setting,
        item.validators,
        item.selectedView,
        item.receivingTypes,
        item.receiverDataTypes,
        item.broadcastingTypes, generateRandomNo(),
        item.accessibleLayoutTypes,
        item.toolbar,
        item.entityName);
}
    
export function generateRandomNo(): string {
		return String((Math.floor(Math.random() * (9999 - 1000)) + 1000));
}
    