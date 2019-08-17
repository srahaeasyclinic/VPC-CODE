// Import the core angular services.
import { ChangeDetectionStrategy, OnInit, ChangeDetectorRef } from "@angular/core";
import { Component } from "@angular/core";
import { EventEmitter } from "@angular/core";

import { ContainerComponent, DraggableComponent, IDropResult } from 'ngx-smooth-dnd';
import { applyDrag, generateItems } from './helper/utils';
import { Operation } from '../model/operation';
import { ITreeNode } from '../model/treeNode';
import { FieldModel } from '../model/fieldmodel';
// ----------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------- //


// export class Setting{
// 	showHeader:boolean;
// 	columnWidth:Number;
// }

// export class Validator{
// 	name:string;
// 	customizable: boolean;
// 	dblength?: number;
// 	userSetlength?: number;
// 	EmailFromat: string;
// 	value:boolean;
// }
// export interface TreeNode {
// 	name:string;
// 	value:any;
// 	required:boolean;
// 	fields: TreeNode[];
// 	dataType:string;
// 	typeOf:string;
// 	controlType: string;
// 	readOnly: boolean;
// 	// public List<Validator> Validators { get; set; }
// 	//only for address
// 	decimalPrecision:number | null;
// 	defaultValue:string;
// 	properties:string;
// 	tabs:TreeNode[];
// 	setting:Setting;
// 	validators:Validator[];
// 	selectedView:any;
// 	receivingTypes: string[];
// 	receiverDataTypes: string[];
// 	broadcastingTypes: string[];	
// 	refId: string;
// 	accessibleLayoutTypes:number[];
// 	toolbar: Operation[];	
// 	entityName:string					
// }

@Component({
	selector: "my-tree",
	inputs: ["rootNode", "selectedNode", "mode", "entityInfo", "resource", "displayRule", "entityName", "selectedField"],
	outputs: ["selectEvents: select", "dropEvents:drop", "closeEvents:close", "editEvents:edit", "addEvents:add"],

	//changeDetection: ChangeDetectionStrategy.OnPush,
	// styleUrls: [ "./tree.component.less" ],
	template:
		`
	<div>
	
	<div class="row" id="page-content-wrapper">
		<my-tree-node 
			class="col-md-12 outer"
			[node]="rootNode"
			[selectedNode]="selectedNode"
			[ngClass]="(firstElement) ? 'firstElement' : 'childElement'"
			[entityInfo] = "entityInfo"
			(select)="selectEvents.emit( $event )"
			(drop)="dropEvents.emit( $event )"
			(close)="closeEvents.emit($event)"
			(edit)="editEvents.emit($event)"
			(add)="addEvents.emit($event)" [mode]= "mode" [resource]="resource" [entityName]="entityName" (applyRuleEmitter)="onApplyRule($event)" [selectedField]="selectedField">
		</my-tree-node>
	</div>
  </div>
	`
})
export class TreeComponent implements OnInit {
	public rootNode: ITreeNode | null;
	public selectedNode: ITreeNode | null;
	public mode: any | null;
	public resource: any | null;
	public firstElement: boolean = false;
	public entityName: string;
	public ruleinfo: any = [];
	public entityInfo: any = [];
	public displayRule: any;
	public applyRule: boolean;
	public elementIndex: any = [];
	public visibility: any;
	public initForRule: boolean;
	public selectedField: FieldModel | null;
	//-----------------------------
	public selectEvents: EventEmitter<ITreeNode>;
	public dropEvents: EventEmitter<ITreeNode>;
	public closeEvents: EventEmitter<ITreeNode>;
	public editEvents: EventEmitter<ITreeNode>;
	public addEvents: EventEmitter<ITreeNode>;

	//------------------------------------

	// I initialize the tree component.
	constructor(private changeRef: ChangeDetectorRef) {
		this.rootNode = null;
		this.selectedNode = null;
		this.mode = null;
		this.resource = null;
		this.entityName = '';
		this.selectEvents = new EventEmitter();
		this.dropEvents = new EventEmitter();
		this.closeEvents = new EventEmitter();
		this.editEvents = new EventEmitter();
		this.addEvents = new EventEmitter();

	}

	ngOnInit() {
		//console.log("my-tree : " + this.entityName);
		//console.log("displayRule === " + this.displayRule);

		if (this.displayRule) {
			this.setItemIndex();
			this.applyRule = true;
			this.buildRulesAgainstCtrl(this.displayRule);
			//this.applyRule = false;
			this.initRule();
		}
		else {
			this.applyRule = false;
		}
		//this.changeRef.reattach();

	}

	// ngDoCheck() {
	// 	console.log('')
	// 	this.changeRef.detectChanges();
	// }

	private initRule() {
		this.elementIndex.forEach(element => {
			//console.log('\n\n\n initRule element ', element);
			this.checkValueExist(element.name, element.value, this.ruleinfo);
		});
	}


	private checkValueExist(argID, argElemntValue, argRule) {
		let _key: any = Object.keys(argRule);
		for (var i = 0; i < _key.length; i++) {
			var keyVal = _key[i];
			this.visibility = this.requestRule(argRule[keyVal], argID, argElemntValue);
			this.setVisibleRule(this.visibility);
		}
	}

	private setVisibleRule(argElement) {
		//console.log('\n setVisibleRule argElement ', argElement);
		if (argElement.isValid != undefined) {
			var sourceNode = this.getNode(argElement);
			sourceNode["visibility"] = argElement.isValid;
			if (sourceNode["visibility"] === false) {
				sourceNode.value = "";
			}
			//
			this.setItemValue(sourceNode.name, sourceNode.value);
			//console.log('this.elementIndex ', this.elementIndex);
			//console.log('\nsetVisibleRule sourceNode ', sourceNode);
			//console.log('this.rootNode ', this.rootNode);
		}
	}

	private getNode(argElement) {
		//console.log(this.getItemIndex(argElement.name));
		if (this.getItemIndex(argElement.name)) {
			var sourceIndex = (this.getItemIndex(argElement.name)).split(',');

			var tempSource = [];
			var buildSource = "";

			sourceIndex.forEach((element, index) => {
				if (index != 0) {
					tempSource.push(this.splitTextNumber(element));
				}
			});

			tempSource.forEach(element => {
				if (element[0][0].indexOf("s") > -1) {
					buildSource += '["fields"][' + element[0][1] + ']';
				}
				else if (element[0][0].indexOf("tg") > -1) {
					buildSource += '["fields"][' + element[0][1] + ']';
				}
				else if (element[0][0].indexOf("ti") > -1) {
					buildSource += '["tabs"][' + element[0][1] + ']';
				}
				else {
					buildSource += '["fields"][' + element[0][0] + ']';
				}
			});

			return eval('this.rootNode' + buildSource);
		}

	}

	private requestRule(validationExp, argID, argElemntValue) {
		//console.log('requestRule called with ', validationExp);
		this.initForRule = false;
		if (validationExp != null) {
			var rule = validationExp.rule;
			var name = validationExp.name;
			var itemIndex = validationExp.index;
			if (rule != null && name != null) {
				var isValid: any;
				// Multiple Rule check				
				for (let index = 0; index < rule.length; index++) {
					let _key = Object.keys(rule[index]);
					if (_key != null) {
						for (var i = 0; i < _key.length; i++) {
							if (argID != null && _key[i].toString() === argID.toString()) {
								isValid = this.singleRuleMatch(rule[0], argElemntValue);
								if (isValid) {
									//break;
									this.initForRule = true;
									break;
								}
							}
						}
					}
				}

				//console.log('validationExp ', validationExp);
				//console.log('isValid ', isValid);
				//console.log('=============********************************========== isValid ', isValid);
				return { isValid: isValid, name: name };
			}
		} else {
			//console.log('=============********************************========== isValid true ', true);
			return { isValid: true, name: name };
		}
	}


	private singleRuleMatch(validationExp, argElemntValue) {
		var _key: any = Object.keys(validationExp);
		//console.log('validationExp ', validationExp);
		if (_key != null) {
			var isValid: any = true;
			for (let index = 0; index < _key.length; index++) {
				const element = _key[index];

				if (argElemntValue != null && this.getItemValue(element) != null && validationExp[element] != null) {
					if (this.getItemValue(element).toString() != validationExp[element].toString()) {
						isValid = false;
						break;
					}
					else {
						if (index == _key.length - 1) {
							isValid = true;
						}
					}
				}
				else {
					isValid = false;
					break;
				}

			}
			return isValid;
		}
	}

	public onApplyRule($event) {
		//console.log('\n\n\n onApplyRule $event ', $event.currentTarget.id);
		this.setItemValue($event.currentTarget.id, $event.currentTarget.value);
		this.checkValueExist($event.currentTarget.id, $event.currentTarget.value, this.ruleinfo);
	}


	private splitTextNumber(inputText) {
		var output = [];
		var json = inputText.split(' ');
		json.forEach(function (item) {
			output.push(item.replace(/\'/g, '').split(/(\d+)/).filter(Boolean));
		});
		return output;
	}

	private getItemIndex(argItemName) {
		let data = this.elementIndex[this.elementIndex.findIndex(t => t.name == argItemName)];
		if (data && data.indexV) {
			return this.elementIndex[this.elementIndex.findIndex(t => t.name == argItemName)].indexV;
		}
	}

	private getItemValue(argItemName) {
		//console.log('\n\ngetItemValue(argItemName) ', argItemName);
		//console.log('this.elementIndex[this.elementIndex.findIndex(t => t.name == argItemName)].value ', this.elementIndex[this.elementIndex.findIndex(t => t.name == argItemName)].value);
		if (this.elementIndex[this.elementIndex.findIndex(t => t.name == argItemName)]) {
			return this.elementIndex[this.elementIndex.findIndex(t => t.name == argItemName)].value;
		}
	}

	private setItemValue(argItemName, argValue) {
		if (this.elementIndex[this.elementIndex.findIndex(t => t.name == argItemName)]) {
			return this.elementIndex[this.elementIndex.findIndex(t => t.name == argItemName)].value = argValue;
		}
		//console.log('setItemValue this.elementIndex ', this.elementIndex);
	}


	private buildRulesAgainstCtrl(data: any) {
		if (data || data != undefined) {
			data.forEach(element => {
				element.targetList.forEach(tgt => {
					let rule = {};
					element.sourceList.forEach(sl => {
						rule[sl.name] = sl.value;
					});
					if (this.ruleinfo[tgt.name] && this.ruleinfo[tgt.name].rule) {
					}
					else {
						this.ruleinfo[tgt.name] = { name: tgt.name, rule: [] };
					}
					this.ruleinfo[tgt.name].rule.push(rule);
					this.ruleinfo[tgt.name].ruleTypeName = element.ruleTypeName;
				});
			});
		}
		//delete this.elementIndex;
		//console.log('ruleinfo  === ', this.ruleinfo);
	}



	private setItemIndex() {
		let treeNode = this.rootNode;
		treeNode["itemIndex"] = "s0";
		treeNode["visibility"] = true;
		this.elementIndex.push({ name: treeNode.name, indexV: treeNode["itemIndex"], value: treeNode.value });
		if (treeNode.fields.length > 0) {
			this.findAndSetIndex(treeNode.fields, treeNode["itemIndex"]);
		}
	}

	private findAndSetIndex(argTreeNode, parentIndex) {
		argTreeNode.forEach((element, index) => {
			if (element.controlType.toLowerCase() === "section") {
				element["itemIndex"] = parentIndex + ",s" + index;
				element["visibility"] = true;
				this.elementIndex.push({ name: element.name, indexV: element["itemIndex"], value: element.value });
				if (element.fields.length > 0) {
					this.findAndSetIndex(element.fields, element["itemIndex"]);
				}
			}
			else if (element.controlType.toLowerCase() === "tabs") {
				element["itemIndex"] = parentIndex + ",tg" + index;
				element["visibility"] = true;
				this.elementIndex.push({ name: element.name, indexV: element["itemIndex"], value: element.value });
				this.findAndSetIndex(element.tabs, element["itemIndex"]);
			}
			else if (element.controlType.toLowerCase() === "tab") {
				element["itemIndex"] = parentIndex + ",ti" + index;
				element["visibility"] = true;
				this.elementIndex.push({ name: element.name, indexV: element["itemIndex"], value: element.value });
				this.findAndSetIndex(element.fields, element["itemIndex"]);
			}
			else {
				element["itemIndex"] = parentIndex + "," + index;
				element["visibility"] = true;
				this.elementIndex.push({ name: element.name, indexV: element["itemIndex"], value: element.value });
			}
		});
	}

}
