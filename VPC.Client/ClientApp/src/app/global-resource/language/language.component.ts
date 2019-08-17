import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { Router, UrlTree, UrlSegment, UrlSegmentGroup, PRIMARY_OUTLET, RouterState, ActivatedRoute, NavigationStart } from '@angular/router';
import { filter, first } from 'rxjs/operators';
import { ResourceService } from '../../resource/resource.service';
import { Location } from '@angular/common';
import { GlobalResourceService } from '../global-resource.service';
import { RoutelocalizationService } from '../../services/routelocalization.service';

@Component({
    selector: 'app-language',
    templateUrl: './language.component.html',
    styleUrls: ['./language.component.css']
  })

  export class LanguageComponent implements OnInit {

    public languages:any = [];
    public selectedLangeKey:string;
    
    constructor(private readonly location: Location,private router: Router, 
        private activatedRoute: ActivatedRoute, 
        private resourceService: ResourceService,
      private globalResourceService: GlobalResourceService,
        private localization: RoutelocalizationService
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
            this.selectedLangeKey=this.localization.getDefaultlanguageKey();
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
        this.selectedLangeKey=lan.key;
        this.localization.setdefaultLanguage(lang);
        this.globalResourceService.getResource();

        let paths: string[] = location.pathname.split('/');
        if (paths&&paths.length>1)
        {
          paths[1] = this.localization.getDefaultlanguageKey().toLocaleLowerCase();
          //this.location.go(paths.join("/"));
           this.location.replaceState(paths.join("/"))
          
        }
      
    }

  }