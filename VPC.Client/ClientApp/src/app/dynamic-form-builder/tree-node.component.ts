// Import the core angular services.
import { ChangeDetectionStrategy, ChangeDetectorRef } from "@angular/core";
import { Component, OnInit, OnChanges, AfterViewInit } from "@angular/core";
import { EventEmitter } from "@angular/core";

import { ContainerComponent, DraggableComponent, IDropResult } from 'ngx-smooth-dnd';
import { applyDrag, generateItems } from './helper/utils';
// Import the application components and services.
// import { TreeNode } from "./tree.component";
import { ITreeNode } from '../model/treeNode';
import { stringify } from "@angular/core/src/render3/util";
import { element } from "@angular/core/src/render3";
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';



// ----------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------- //

@Component({
	selector: "my-tree-node",
	templateUrl: './tree-node.component.html',
	styleUrls: ["./tree-node.component.css"],
	inputs: ["node", "selectedNode", "mode", "resource", "ruleinfo", "entityInfo", "firstElement"],
	outputs: ["selectEvents: select", "dropEvents:drop", "closeEvents:close", "editEvents:edit", "addEvents:add"],
	// host: {
	// 	"[class.selected]": "( node === selectedNode )"
	// },
	changeDetection: ChangeDetectionStrategy.OnPush,
	// styleUrls: [ "./tree-node.component.less" ],

})
export class TreeNodeComponent implements OnInit, AfterViewInit {

	public node: ITreeNode | null;
	public mode: any | null;
	public resource: any | null;
	public selectedNode: ITreeNode | null;
	public displayRule: any;
	public selectEvents: EventEmitter<ITreeNode>;
	public dropEvents: EventEmitter<ITreeNode>;

	public closeEvents: EventEmitter<ITreeNode>;
	public editEvents: EventEmitter<ITreeNode>;
	public addEvents: EventEmitter<ITreeNode>;

	public SelectedMetalistdata: string = '';

	public firstElement = true;

	public applyRule: boolean;
	//private elemntValue: any;

	public visibility: boolean;
	private initForRule: boolean;
	private requestForRule: boolean;


	//droppedData: string;
	// I initialize the tree node component.


	arrUserData: any;
	arrayList: any;
	public ruleinfo: any = [];
	public entityInfo: any = [];

	constructor(public globalResourceService:GlobalResourceService, private changeRef: ChangeDetectorRef) {

		this.node = null;
		this.mode = null;
		this.resource = null;

		this.applyRule = true;

		this.selectedNode = null;
		this.selectEvents = new EventEmitter();
		this.dropEvents = new EventEmitter();

		this.closeEvents = new EventEmitter();
		this.editEvents = new EventEmitter();
		this.addEvents = new EventEmitter();
		
	}
	makeCamelCase(node: ITreeNode) {
		if (node == null || node.name == null) return "["+node.name+"]";
		if (node.controlType.toLocaleLowerCase() == "section" || node.controlType.toLocaleLowerCase() == "custom") {
			return node.name;
		};
		try {
          return this.globalResourceService.getResourceValueByKey(node.name);
		} catch (error) {
		}
	}

	ngOnInit() {
		//  this.resource = this.globalResourceService.getGlobalResources();
		//this.setRule();
		//console.log('Tree Node display rule ', this.displayRule);
		//this.rules = this.displayRule;

		// this.httpService.get('./assets/rule.json').subscribe(
		// 	data => {
		//if(this.displayRule);
		//console.log('rule there ', this.displayRule)
		// if (this.displayRule) {
		// 	this.applyRule = true;
		// 	this.buildRulesAgainstCtrl(this.displayRule);
		// 	//this.applyRule = false;
		// }
		// else {
		// 	this.applyRule = false;
		// }

		// 	}
		//   );
		//console.log('Node value'+JSON.stringify(this.node))

	}

	ngAfterViewInit() 
	{
		this.initForRule = true;
		this.requestForRule = false;
		
		if (this.ruleinfo) {
			let _ruleRequired = !this.initRule(this.ruleinfo[this.node.name]);

			//console.log('__________________ruleRequired ', _ruleRequired);

			let item: any = document.getElementById('tn_' + (this.node.name));
			let itemInput: any = document.getElementById((this.node.name));

			//console.log('____________________________________________for the item ', item);

			if (item && item.classList && itemInput) {
				if (_ruleRequired) {
					itemInput.value = "";
					item.classList.add('hidden');
				}
				else {
					itemInput.value = "";
					item.classList.remove('hidden');
				}
			}
		}
	}

	ngOnChanges() {

	}

	//Applying rule based on value change in elements Started

	public onChangeEvent($evt: any) {
		///console.log('$evt ', $evt);
		if(this.ruleinfo)
		{
			this.checkValueExist($evt.currentTarget.id, $evt.currentTarget.value, this.ruleinfo);
		}
	}

	private checkValueExist(argID, argElemntValue, argRule) {
		
		let _key: any = Object.keys(argRule);	

		for(var i = 0; i < _key.length; i++){
			var keyVal = _key[i];		    
			this.visibility = this.requestRule(argRule[keyVal], argID, argElemntValue);
			let item: any = document.getElementById('tn_' + (keyVal));
			let itemInput: any = document.getElementById((keyVal));
			
			if (item) {
				item.value = "";
				if (this.visibility) {
					itemInput.value = "";
					item.classList.remove('hidden');
					break;
				}
				else if (this.visibility != undefined) {
					itemInput.value = "";
					item.classList.add('hidden');
				}
			}
		}

		// this.visibility = this.requestRule(argRule[_key], argID, argElemntValue);
		// let item: any = document.getElementById('tn_' + (_key));
		// let itemInput: any = document.getElementById((_key));
		// //console.log('this.visibility ', this.visibility);
		// if (item) {
		// 	item.value = "";
		// 	if (this.visibility) {
		// 		itemInput.value = "";
		// 		item.classList.remove('hidden');
		// 	}
		// 	else if (this.visibility != undefined) {
		// 		itemInput.value = "";
		// 		item.classList.add('hidden');
		// 	}
		// }
	}

	allowDrop(ev) {
		//console.log('ev ', ev);
		ev.preventDefault();
	}


	requestRule(validationExp, argID, argElemntValue) {
		//console.log('requestRule called with ', validationExp);
		this.initForRule = false;
		if (validationExp != null) {
			var rule = validationExp.rule;
			var name = validationExp.name;			
			if (rule != null && name != null) {
				var isValid: any;
				// Multiple Rule check				
				for (let index = 0; index < rule.length; index++) 
				{
					let _key = Object.keys(rule[index]);
					if (_key != null) 
					{
						for(var i = 0; i< _key.length; i++)
						{
							if (argID != null && _key[i].toString() === argID.toString()) {								
								isValid = this.singleRuleMatch(rule[0], argElemntValue);
								if (isValid) {
									break;
								}
							}
						}
						
					}
				}
				
				// if (!isValid) {
				// 	var item: any = document.getElementById(name);
				// 	if (item) {
				// 		//item.value = "";
				// 		//console.log('item', item);
				// 	}
				// }
				//console.log('validationExp ', validationExp);
				//console.log('isValid ', isValid);
				//console.log('=============********************************========== isValid ', isValid);
				return isValid;
			}
		} else {
			//console.log('=============********************************========== isValid true ', true);
			return true;
		}
	}


	singleRuleMatch(validationExp, argElemntValue) {
		var _key: any = Object.keys(validationExp);
		if (_key != null) 
		{
			var isValid: any = true;
			for (let index = 0; index < _key.length; index++) {
				const element = _key[index];				
				var item: any = document.getElementById(element);
				
				/*if (item && argElemntValue) {
					console.log(argElemntValue.toLowerCase());
					//console.log(validationExp[element].toLowerCase());
					if (argElemntValue.toLowerCase() != validationExp[element].toLowerCase()) {
						isValid = false;
						break;
					}
					if (index == _key.length - 1) {
						isValid = true;
					}
				} else {
					isValid = false;
					break;
				}*/

				if (item != null) 
				{
					if (argElemntValue != null) 
					{
						if (argElemntValue.toLowerCase() != validationExp[element].toLowerCase()) {
							isValid = false;
							//break;
						}						
						else{
							if (index == _key.length - 1) {
								isValid = true;
							}
						}
						
					} 
					else 
					{						
						if (this.entityInfo[element.toLowerCase()] != validationExp[element]) {
							isValid = false;							
							break;
						}
					}
				} 
				else 
				{
					isValid = false;
					break;
				}
			}
			
			return isValid;
		}
	}
	//dont delete
	// detectChange() {
	// }\\

	initRule(validationExp) {
		if (validationExp != null) {
			//console.log('initRule called in tree-node with ', validationExp);
			var rule = validationExp.rule;
			var name = validationExp.name;
			//console.log('name ', name);//Nationaility
			//console.log('argID ', argID);//Nationaility
			if (rule && name) {
				var isValid: any;
				// Multiple Rule check
				for (let index = 0; index < rule.length; index++) {
					let _key = Object.keys(rule[index]);
					//console.log('_key ', _key);//Gender
					if (_key) {
						isValid = this.singleRuleMatch(rule[index], null);
						if (isValid) {
							break;
						}
					}
				}
				// if (!isValid) {
				// 	var item: any = document.getElementById(name);
				// 	if (item) {
				// 		//item.value = "";
				// 		//console.log('item', item);
				// 	}
				// }
				//console.log('validationExp ', validationExp);
				//console.log('isValid ', isValid);
				return isValid;
			}
		} else {
			return true;
		}
	}
	//

	public isRequired(node:any):boolean
	{
		//console.log('JSON of-' + JSON.stringify(node));
		//console.log(this.mode);
		
		if (this.mode==2) {
			return false;
		}
		if (node.required)
		{
			return true;
			
		}

		if ((node == undefined ||node.validators == undefined))
		{
			return false;
		}
		//console.log('JSON of-'+JSON.stringify(node.validators));
		let isrequired;	
		let objnode=node.validators.filter(element => element.name.toLowerCase() === "requiredvalidator"||element.name.toLowerCase() === "requiredfieldvalidator");
		if (objnode!=null && objnode.length>0 && objnode[0].options!=undefined)
		{
			objnode[0].options.forEach(element => {
				
				isrequired = (element!=undefined && element.name.toLowerCase() === 'required' && element.value!=undefined && element.value === true)?true:false;
				
		    });
		}
		
		return isrequired;

	}
	// generateResourceName(word)
	// {
	//    if (!word) return word;
	//    return word[0].toLowerCase() + word.substr(1);
	//  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
