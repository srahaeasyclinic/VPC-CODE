import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { Router, UrlTree, UrlSegment, UrlSegmentGroup, PRIMARY_OUTLET, RouterState, ActivatedRoute, NavigationStart } from '@angular/router';
import { filter, first } from 'rxjs/operators';
import { ResourceService } from '../../resource/resource.service';
import { Location } from '@angular/common';
import {GlobalResourceService} from '../global-resource.service';
@Component({
    selector: 'app-language',
    templateUrl: './language.component.html',
    styleUrls: ['./language.component.css']
  })

  export class LanguageComponent implements OnInit {

    public languages:any = [];
    
    constructor(private router: Router, 
        private activatedRoute: ActivatedRoute, 
        private resourceService: ResourceService,
        private location: Location,
        private globalResourceService:GlobalResourceService,
        ) {
      }

      ngOnInit()
      {
        this.getLanguages();  
      }

      private getLanguages() {
        this.resourceService.getLanguages().subscribe(data => {
          if (data) {
            this.languages = [...data.result];
          }
        }, 
        error => console.log(error));
      }
    
      public onLanguageClick(lan)
      {
        var lang={
          key:lan.key,
          text:lan.text,
        };        
        localStorage.setItem('langInfo', JSON.stringify(lang));
        this.globalResourceService.getResource();
        window.location.reload();
        //this.router.navigate(["../"], { relativeTo: this.activatedRoute });
        //this.router.navigate([],{ relativeTo: this.activatedRoute });
        //console.log(this.router.url);
    }

  }