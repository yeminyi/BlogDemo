import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlogModule } from "./blog/blog.module";
const routes: Routes = [
  { path: 'blog', loadChildren: ()=>BlogModule },
  { path: '**', redirectTo: 'blog' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
