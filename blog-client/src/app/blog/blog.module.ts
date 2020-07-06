import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlogRoutingModule } from './blog-routing.module';
import { MaterialModule } from '../shared/material/material.module';
import { BlogAppComponent } from './blog-app.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [BlogAppComponent, SidenavComponent, ToolbarComponent],
  imports: [
    CommonModule,
    BlogRoutingModule,
    MaterialModule,
    HttpClientModule,
  ]
})
export class BlogModule { }
