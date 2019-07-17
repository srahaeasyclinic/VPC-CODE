import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';

@Component({
  selector: 'app-rolesequrity',
  templateUrl: './rolesequrity.component.html'
})
export class RoleSecurityComponent implements OnInit {  
  entityName: string;
  public resource = Resource;
  
  constructor(private activatedRoute: ActivatedRoute,private router: Router,
    private globalResourceService: GlobalResourceService  ) { }

  navLinks=[ 
    {path:'entitysecurity', label:'Entity'},
    {path:'workflowsecurity', label:'WorkFlow'},
  ];
 

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();      
    this.activatedRoute.parent.params.subscribe((params: Params) => {
      this.entityName = params['name'];
    });
  } 
  getResourceByKey(key: any) {
    if(this.resource[this.generateResourceName(key)]){
      return this.resource[this.generateResourceName(key)];
    }else{
      return key;
    }
  }
  generateResourceName(word)
  {
     if (!word) return word;
     return word[0].toLowerCase() + word.substr(1);
   }
    

}
