import { Component, OnInit, Pipe, PipeTransform, OnChanges } from '@angular/core';
import { Router, UrlTree, UrlSegment, UrlSegmentGroup, PRIMARY_OUTLET, RouterState, ActivatedRoute, NavigationStart, Params } from '@angular/router';
import { filter, first } from 'rxjs/operators';
import { BredcrumService } from '../bread-crumb/bredcrum.service';
import { CommonService } from '../services/common.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent implements OnInit, OnChanges {
  objectkeys = Object.keys;
  url = '';
  tree: UrlTree;
  fragment = '';
  queryParams = {};
  // primary outlet
  primary: UrlSegmentGroup;
  // secondary outlet
  sidebar: UrlSegmentGroup;
  public segmenttree: any[];
  private lastActive: string;
  private breadcrumbComplete: boolean = false;
  public resource: any;
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private breadcrumService: BredcrumService,
    public commonService: CommonService,
    private globalResourceService: GlobalResourceService) { }

  ngOnInit() {
    this.resource=this.globalResourceService.getGlobalResources();
    //console.log('ngOnInit called');
    this.processUrl(this.router.url);
    this.router.events.pipe(filter(value => value instanceof NavigationStart)).subscribe((value: any) => {
      this.processUrl(value.url);
    });
  }

  ngOnChanges() {
    //console.log('onChange');
    //console.log('this.commonService.getDisplayName4Breadcrumb() ', this.commonService.getDisplayName4Breadcrumb());
  }

  processUrl(url: string) {
    this.segmenttree = [];
    this.tree = this.router.parseUrl(url);
    if (this.tree.root.numberOfChildren > 0) {
      this.primary = this.tree.root.children[PRIMARY_OUTLET];
      this.primary.segments.forEach(element => {
        this.segmenttree.push({
          labelName: element.path, //nedd to place a getNameFunction;
          url: url.split(element.path)[0] + element.path,
          itemName: null
        });
        //console.log('url ', url);
        //console.log('element.path ', element.path);
      });
      this.segmenttree[0].labelName = 'Home';
      this.segmenttree[0].url = '/home';
      this.segmenttree[0].itemName = null;
    }
    //console.log('this.segmenttree ', this.segmenttree);
    this.processBreadcrumb();
  }

  processBreadcrumb() {
    this.activatedRoute.params.subscribe((urlPath) => {
      this.lastActive = urlPath['name'];
    });
    if (this.lastActive) {
      //console.log('this.lastActive ', this.lastActive);
      let lastIndex = this.segmenttree.findIndex(t => (t.labelName === this.lastActive));
      //console.log('lastIndex ', lastIndex);

      if (lastIndex > 0) {
        var spliced = this.segmenttree.splice((lastIndex + 1), (this.segmenttree.length - lastIndex));
      }
      if (spliced && spliced.length > 0) {
        let lastElement = spliced.find(t => t.labelName === this.lastActive);

        if (lastElement) {
          this.segmenttree.push(lastElement);
          if (spliced.length > 1) {
            this.segmenttree[this.segmenttree.length - 1].isActive = true;
          }
        }
        else {
          this.segmenttree[this.segmenttree.length - 1].isActive = true;
        }

        //console.log('this.commonService.getDisplayName4Breadcrumb() ', this.commonService.getDisplayName4Breadcrumb());
        //this.segmenttree[this.segmenttree-1].itemName = this.commonService.getDisplayName4Breadcrumb();
      }
      //console.log('spliced ', spliced);
      //console.log('this.segmenttree ', this.segmenttree);
    }
  }


  openPageWithBreadcrumb(value: any) {
    //console.log(value);
    this.router.navigate([value.url]);
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