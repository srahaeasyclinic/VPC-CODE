import { Injectable } from '@angular/core';


@Injectable(
    {
        providedIn: 'root'
    }
)
export class HierarchyDropdownService {
    private Dropdowndata = [];
    constructor() { }
    

  dropdowndatagrouping(orginalarr:[],groupbyproparty:string):any[]
  {

    let groupdata = this.getgroupbydata(orginalarr, groupbyproparty);
    this.decorateobjecthierarchy(groupdata, orginalarr);

    return this.Dropdowndata;

  }
  
private getgroupbydata(objectArray:any, property:string) {
  return objectArray.reduce(function (acc, obj) {
    var key = obj[property];
    if (!acc[key]) {
      acc[key] = [];
    }
    acc[key].push(obj);
    return acc;
  }, {});
    }
    
 private decorateobjecthierarchy(grouparray:any,originalarray:any)
  {
    if (grouparray!=null||grouparray!=undefined)
    {
 
      this.Dropdowndata = [];
      Object.keys(grouparray).forEach(element => {

        var name = originalarray.filter(x => x.internalId == element)
        
      if (name && name[0])
      {
        this.Dropdowndata.push({ group: { id: element, name: name[0].text, totalchild:grouparray[element].length }, items: grouparray[element] })
      
      }
      });

      var Ischilditself = originalarray.filter(x => x.parentId == null);
      var haschild = 0;
      Ischilditself.forEach(s => {

        var haschild = originalarray.filter(x => x.parentId == s.internalId).length;
        if (haschild==0||haschild==undefined)
        {
           this.Dropdowndata.push({ group: { id: s.internalId, name: s.text, totalchild:0 }, items: null })
        }
        
      });
        
      //console.log('final result',data)

      }
      
      
  }

 
}