import { Operation } from '../model/operation';

export class Setting {
    showHeader: boolean;
    columnWidth: Number;
    linkSetting: LinkSetting;
}
export class LinkSetting {
    type: Number;
    url: string;
}

export class Validator {
    name: string;
    customizable: boolean;
    dblength?: number;
    userSetlength?: number;
    EmailFromat: string;
    value: boolean;
}
////////////////Interface of Treenode///////////////////////////////////////////////
export interface ITreeNode {
    name: string;
    value: any;
    required: boolean;
    // PickListId: string;
    fields: ITreeNode[];
    dataType: string;
    displayName: string; // Added by Soma
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
    selectedFormOrList:any;
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
    supportedQuickAddModes: number[];
    isQuickAddSupported: boolean;
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////// Implement interface to class////////////////////////////////////////////////////////////
class TreeNode implements ITreeNode {
    name: string;
    value: any;
    required: boolean;
    PickListId: string;
    fields: ITreeNode[];
    dataType: string;
    displayName: string; // Added by Soma
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
    selectedFormOrList:any;
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
    supportedQuickAddModes: number[];
    isQuickAddSupported: boolean;
    constructor(
        name: string = "",
        value: any = "",
        required: boolean = false,
        fields: TreeNode[] = [],
        dataType: string = "",
        typeOf: string = "",
        displayName: string = "", // Added by Soma
        controlType: string = "",
        readOnly: boolean = false,
        decimalPrecision?: number,
        defaultValue: string = "",
        properties: string = "",
        tabs: TreeNode[] = [],
        setting?: Setting,
        validators?: Validator[],
        selectedView?: any,
        selectedFormOrList?:any,
        receivingTypes: string[] = [],
        receiverDataTypes: string[] = [],
        broadcastingTypes: string[] = [],
        refId: string = "",
        //IsApplicableForFilter?: boolean,
        // draggedItem?: boolean,
        // isQueryable?: boolean,
        // PickListId?:string,
        accessibleLayoutTypes: number[] = [],
        toolbar: Operation[] = [],
        entityName: string = "",
        supportedQuickAddModes: number[] = [],
        isQuickAddSupported: boolean = false
    ) {
        this.name = name;
        this.value = value;
        this.required = required;
        this.fields = fields;
        this.dataType = dataType;
        this.displayName = displayName; // Added by Soma
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
        this.selectedFormOrList=selectedFormOrList;
        this.receivingTypes = receivingTypes;
        this.receiverDataTypes = receiverDataTypes;
        this.broadcastingTypes = broadcastingTypes;
        this.refId = refId;
        this.accessibleLayoutTypes = accessibleLayoutTypes;
        this.toolbar = toolbar;
        this.entityName = entityName;
        this.supportedQuickAddModes = supportedQuickAddModes;
        this.isQuickAddSupported = isQuickAddSupported;
        // this.IsApplicableForFilter = IsApplicableForFilter;
        // this.draggedItem = draggedItem;
        //this.isQueryable = isQueryable;
        //this.PickListId = PickListId;
    }

}
/////////////////////////////////////// Instanciate for treeNode///////////////////////////////////////////////////////////////////
export function getTreenodeinstanceWithObject(item?: any) {
    console.log('getTreenodeinstanceWithObject '+JSON.stringify(item));
    if (item === null) {
        return new TreeNode();
    }

    var setting = item.setting==null?  getSettingsObj():item.setting;
    return new TreeNode(item.name,
        item.value,
        item.required,
        item.fields,
        item.dataType,
        item.displayName, // Added by Soma
        item.typeOf,
        item.controlType,
        item.readOnly,
        item.decimalPrecision,
        item.defaultValue,
        item.properties,
        item.tabs,
        setting,
        item.validators,
        item.selectedView,
        item.selectedFormOrList,
        item.receivingTypes,
        item.receiverDataTypes,
        item.broadcastingTypes, generateRandomNo(),
        item.accessibleLayoutTypes,
        item.toolbar,
        item.entityName, item.supportedQuickAddModes, item.isQuickAddSupported);

    function getSettingsObj() {
        var settingObj = new Setting();
        settingObj.columnWidth = 12;
        settingObj.showHeader = true;
        settingObj.linkSetting = new LinkSetting();
        settingObj.linkSetting.type = 1;
        settingObj.linkSetting.url = "";
        return settingObj;
    }
}

export function generateRandomNo(): string {
    return String((Math.floor(Math.random() * (9999 - 1000)) + 1000));
}
