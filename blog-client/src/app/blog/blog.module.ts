import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlogRoutingModule } from './blog-routing.module';
import { MaterialModule } from '../shared/material/material.module';
import { BlogAppComponent } from './blog-app.component';


@NgModule({
  declarations: [BlogAppComponent],
  imports: [
    CommonModule,
    BlogRoutingModule,
    MaterialModule
  ]
})
export class BlogModule { }
