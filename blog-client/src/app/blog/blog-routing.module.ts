import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlogAppComponent } from './blog-app.component';
import { PostListComponent } from './components/post-list/post-list.component';
import { RequireAuthenticatedUserRouteGuard } from '../shared/oidc/require-authenticated-user-route.guard';
import { WritePostComponent } from './components/write-post/write-post.component';
import { PostTableComponent } from './components/post-table/post-table.component';
import { EditPostComponent } from './components/edit-post/edit-post.component';
import { PostDetailComponent } from './components/post-detail/post-detail.component';
const routes: Routes = [
  {
    path: '', component: BlogAppComponent,
    children: [
      { path: 'post-list', component: PostListComponent },
      { path: 'post-table', component: PostTableComponent },
      {
        path: 'write-post', component: WritePostComponent,
        canActivate: [RequireAuthenticatedUserRouteGuard]
      },
      {
        path: 'edit-post/:id', component: EditPostComponent,
        canActivate: [RequireAuthenticatedUserRouteGuard]
      },
      { path: 'post-detail/:id', component: PostDetailComponent },
      { path: '**', redirectTo: 'post-list'}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlogRoutingModule { }
