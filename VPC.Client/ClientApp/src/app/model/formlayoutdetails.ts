import { SelectedItem } from './selecteditem';
import { OrderDetails } from './orderdetails';
import { SearchProperties } from './searchproperties';
import { RowLevelOperations } from './rowleveloperations';
import { Operation } from './operation';
// import { TreeNode, Validator, Setting } from '../dynamic-form-builder/tree.component';
import {ITreeNode,Validator, Setting} from '../model/treeNode'

export class FormLayoutDetails implements ITreeNode {
    supportedQuickAddModes: number[];
    isQuickAddSupported:boolean;
    typeOf: string;
    validators: Validator[];
    setting: Setting;
    controlType: string;
    readOnly: boolean;
    dataType: string;
    displayName: string; //Added by Soma
    decimalPrecision: number;
    defaultValue: string;
    properties: string;
    name: string;
    value: any;
    required: boolean;
    fields: ITreeNode[];
    tabs: any[];
    selectedView: any;
    selectedFormOrList:any;
    receivingTypes: string[];
    receiverDataTypes: string[];
    broadcastingTypes: string[];
    refId: string;
    accessibleLayoutTypes: number[];
    toolbar: Operation[];
    entityName: string;
}