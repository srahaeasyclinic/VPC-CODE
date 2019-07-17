// Import the core angular services.
import { ChangeDetectionStrategy, OnInit, ChangeDetectorRef } from "@angular/core";
import { Component } from "@angular/core";
import { EventEmitter } from "@angular/core";

import { ContainerComponent, DraggableComponent, IDropResult } from 'ngx-smooth-dnd';
import { applyDrag, generateItems } from './helper/utils';
import { Operation } from '../model/operation';
import {ITreeNode} from '../model/treeNode'
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
	inputs: [ "rootNode", "selectedNode", "mode", "entityInfo", "resource", "displayRule"],
	outputs: [ "selectEvents: select",  "dropEvents:drop", "closeEvents:close", "editEvents:edit", "addEvents:add"],

	changeDetection: ChangeDetectionStrategy.OnPush,
	// styleUrls: [ "./tree.component.less" ],
	template:
	`
	<div>
	
	<div class="row">
		<my-tree-node 
			class="col-md-12 outer"
			[node]="rootNode"
			[selectedNode]="selectedNode"
			[ngClass]="(firstElement) ? 'firstElement' : 'childElement'"
			[entityInfo] = "entityInfo"
			[ruleinfo] = "ruleinfo"
			(select)="selectEvents.emit( $event )"
			(drop)="dropEvents.emit( $event )"
			(close)="closeEvents.emit($event)"
			(edit)="editEvents.emit($event)"
			(add)="addEvents.emit($event)" [mode]= "mode" [resource]="resource">
		</my-tree-node>
	</div>
  </div>
	`
})
export class TreeComponent implements OnInit {
	public rootNode: ITreeNode | null;
	public selectedNode: ITreeNode | null;
	public mode:any|null;
	public resource:any|null;
	public firstElement: boolean = false;

	public ruleinfo: any = [];
	public entityInfo: any = [];
	public displayRule: any;
	public applyRule: boolean;
	
	//-----------------------------
	public selectEvents: EventEmitter<ITreeNode>;
	public dropEvents:EventEmitter<ITreeNode>;
	public closeEvents:EventEmitter<ITreeNode>;
	public editEvents:EventEmitter<ITreeNode>;
	public addEvents:EventEmitter<ITreeNode>;
	//------------------------------------

	// I initialize the tree component.
	constructor(private changeRef: ChangeDetectorRef) {
		this.rootNode = null;
		this.selectedNode = null;
		this.mode = null;
		this.resource = null;
		this.selectEvents = new EventEmitter();
		this.dropEvents = new EventEmitter();
		this.closeEvents = new EventEmitter();
		this.editEvents = new EventEmitter();
		this.addEvents = new EventEmitter();
		
	}

	ngOnInit()
	{
		if (this.displayRule) {
			this.applyRule = true;
			this.buildRulesAgainstCtrl(this.displayRule);
			//this.applyRule = false;
		}
		else {
			this.applyRule = false;
		}
		this.changeRef.reattach();
	}


	buildRulesAgainstCtrl(data: any) {
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
						this.ruleinfo[tgt.name] = { name: tgt.name, rule: [] }
					}
					this.ruleinfo[tgt.name].rule.push(rule);
					this.ruleinfo[tgt.name].ruleTypeName = element.ruleTypeName;
				});
			});
		}
	}

}
