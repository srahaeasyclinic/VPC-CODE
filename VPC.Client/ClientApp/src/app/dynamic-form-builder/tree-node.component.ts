// Import the core angular services.
import { ChangeDetectionStrategy, ChangeDetectorRef, DoCheck } from "@angular/core";
import { Component, OnInit, OnChanges, AfterViewInit, Output } from "@angular/core";
import { EventEmitter } from "@angular/core";
import{ActivatedRoute} from '@angular/router';

import { ContainerComponent, DraggableComponent, IDropResult } from 'ngx-smooth-dnd';
import { applyDrag, generateItems } from './helper/utils';
// Import the application components and services.
// import { TreeNode } from "./tree.component";
import { ITreeNode } from '../model/treeNode';
import { stringify } from "@angular/core/src/render3/util";
import { element } from "@angular/core/src/render3";
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';
import { SharedTreeService } from "./service/sharedtree.service";
import { FieldModel } from '../model/fieldmodel';


// ----------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------- //

@Component({
	selector: "my-tree-node",
	templateUrl: './tree-node.component.html',
	styleUrls: ["./tree-node.component.css"],
	inputs: ["node", "selectedNode", "mode", "resource", "entityInfo", "firstElement","entityName", "selectedField"],
	outputs: ["selectEvents: select", "dropEvents:drop", "closeEvents:close", "editEvents:edit", "addEvents:add"],
	providers: [SharedTreeService],
	// host: {
	// 	"[class.selected]": "( node === selectedNode )"
	// },
	changeDetection: ChangeDetectionStrategy.OnPush,
	// styleUrls: [ "./tree-node.component.less" ],

})
export class TreeNodeComponent implements OnInit, DoCheck {

	public node: ITreeNode | null;
	public mode: any | null;
	public resource: any | null;
	public selectedNode: ITreeNode | null;
	public selectedField: FieldModel | null;
	public selectEvents: EventEmitter<ITreeNode>;
	public dropEvents: EventEmitter<ITreeNode>;

	public closeEvents: EventEmitter<ITreeNode>;
	public editEvents: EventEmitter<ITreeNode>;
	public addEvents: EventEmitter<ITreeNode>;
	//public applyRuleEmitter: EventEmitter<ITreeNode>;
	public SelectedMetalistdata: string = '';
	

	public firstElement = true;

	//private elemntValue: any;
	public entityName:string;


	@Output() applyRuleEmitter = new EventEmitter<any>();

	//droppedData: string;
	// I initialize the tree node component.


	arrUserData: any;
	arrayList: any;
	public entityInfo: any = [];

	constructor(public globalResourceService: GlobalResourceService,
		private sharedTreeService: SharedTreeService,        
        private route: ActivatedRoute, public changeRef: ChangeDetectorRef
	) {
	

		this.node = null;
		this.mode = null;
		this.resource = null;

		this.selectedNode = null;
		this.selectEvents = new EventEmitter();
		this.dropEvents = new EventEmitter();

		this.closeEvents = new EventEmitter();
		this.editEvents = new EventEmitter();
		this.addEvents = new EventEmitter();

	}
	makeCamelCase(node: ITreeNode) {
		if (node == null || node.name == null) return "[" + node.name + "]";
		if (node.controlType.toLocaleLowerCase() == "section" || node.controlType.toLocaleLowerCase() == "custom") {
			return node.name;
		};
		try {
			return this.globalResourceService.getResourceValueByKey(node.name);
		} catch (error) {
		}
	}

	ngOnInit() {
		this.entityName=this.entityName.toLowerCase();
		this.changeRef.detectChanges();
	}

	ngDoCheck(){
		//console.log('CD');
		this.changeRef.detectChanges();
	}

	public onChangeEvent($evt: any) {
		this.applyRuleEmitter.emit($evt);
	}

	public onApplyRule($event)
	{
		this.applyRuleEmitter.emit($event);
	}


	allowDrop(ev) {
		//console.log('ev ', ev);
		//ev.preventDefault();
		//console.log('event ', event);
		this.changeRef.detectChanges();
		(<any>event).stopPropagation();
		(<any>event).preventDefault();
	}

	setDropZone(ev){
		//console.log('eve ', ev);
		ev['dragover'] = true;
		this.changeRef.detectChanges();
		(<any>event).stopPropagation();
		(<any>event).preventDefault();
	}

	hideDropZone(ev)
	{
		ev['dragover'] = false;
		this.changeRef.detectChanges();
		(<any>event).stopPropagation();
		(<any>event).preventDefault();
	}


	public isRequired(node: any): boolean {
		//console.log('JSON of-' + JSON.stringify(node));
		//console.log(this.mode);

		if (this.mode == 2) {
			return false;
		}
		if (node.required) {
			return true;

		}

		if ((node == undefined || node.validators == undefined)) {
			return false;
		}
		//console.log('JSON of-'+JSON.stringify(node.validators));
		let isrequired;
		let objnode = node.validators.filter(element => element.name.toLowerCase() === "requiredvalidator" || element.name.toLowerCase() === "requiredfieldvalidator");
		if (objnode != null && objnode.length > 0 && objnode[0].options != undefined) {
			objnode[0].options.forEach(element => {

				isrequired = (element != undefined && element.name.toLowerCase() === 'required' && element.value != undefined && element.value === true) ? true : false;

			});
		}

		return isrequired;

	}
	
	getResourceValue(key) {
        return this.globalResourceService.getResourceValueByKey(key.replace('.', '_').toLowerCase());
	}
	onselect(event) {
		// this.sharedTreeService.from = this
		this.selectEvents.emit(event)
	}
	ondrop(event) {
		// this.sharedTreeService.to = this
		this.dropEvents.emit(event)
	}
}
