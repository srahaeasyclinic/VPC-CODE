import { TreeNode } from "@angular/router/src/utils/tree";

export const applyDrag = (arr, dragResult) => {
	const { removedIndex, addedIndex, payload } = dragResult;
	if (removedIndex === null && addedIndex === null) return arr;

	const result = [...arr];
	let itemToAdd = payload;

	if (removedIndex !== null) {
		itemToAdd = result.splice(removedIndex, 1)[0];
	}

	if (addedIndex !== null) {
		result.splice(addedIndex, 0, itemToAdd);
	}

	return result;
};

export const generateItems = (count, creator) => {
	const result = [];
	for (let i = 0; i < count; i++) {
		result.push(creator(i));
	}
}


export const predifinedType =()=> {
	let list: string[] = ["Section", "Tabs" ];
	const result =[];
	list.forEach(element => {
		var myObj = {
			name: element,
			value: "",
			required: false, //need to change
			dataType:element,
			fields: [],
			controlType:element,
			decimalPrecision:null,
			defaultValue:"",
			properties:"",
			tabs:[]
		}
		if(element=="Tabs"){

			var tabSection = {
				name: "First tab",
				value: "",
				required: false, //need to change
				dataType:"Section",
				fields: [],
				controlType:"Section",
				decimalPrecision:null,
				defaultValue:"",
				properties:"",
				tabs:[]
			}
			myObj.tabs.push(tabSection);
		}
		result.push(myObj);
	});
	return result;
}

export const nodeName = (str:string) => {
	if(str!=""){
		var result = str.split(".");
		if(result!=null && result.length > 0){
			return result[result.length-1];
		}
	}
	return "";
}