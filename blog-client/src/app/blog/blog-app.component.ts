import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-blog-app',
  template: `<app-sidenav></app-sidenav>`,
  styles: [
  ]
})
export class BlogAppComponent implements OnInit {

  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon('baseline-more_vert', sanitizer.bypassSecurityTrustResourceUrl('/assets/user-24px.svg'));
    iconRegistry.addSvgIcon('baseline-menu', sanitizer.bypassSecurityTrustResourceUrl('/assets/baseline-menu-24px.svg'));    
    iconRegistry.addSvgIcon('baseline-add', sanitizer.bypassSecurityTrustResourceUrl('/assets/baseline-add-24px.svg'));
    iconRegistry.addSvgIcon('delete', sanitizer.bypassSecurityTrustResourceUrl('/assets/delete-24px.svg'));    
    iconRegistry.addSvgIcon('edit', sanitizer.bypassSecurityTrustResourceUrl('/assets/edit-24px.svg'));
    iconRegistry.addSvgIcon('details', sanitizer.bypassSecurityTrustResourceUrl('/assets/text_snippet-24px.svg'));
   }


  ngOnInit(): void {
  }

}
