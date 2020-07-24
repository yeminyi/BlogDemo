import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Post } from '../../models/post';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from '../../services/post.service';
import { OpenIdConnectService } from '../../../shared/oidc/open-id-connect.service';
import { ConfirmDialogComponent } from 'src/app/blog/components/confirm-dialog/confirm-dialog.component';
import {MatDialog} from '@angular/material/dialog';
import { take } from 'rxjs/operators';
@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss']
})
export class PostDetailComponent implements OnInit {
  post: Post;
  constructor(private route: ActivatedRoute,
    private postService: PostService,
    private router: Router,
    private openIdConnectService: OpenIdConnectService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      let id = +params['id'];
      if (!id) {
        id = 1;
      }
      this.post = null;

      this.postService.getPostById(id).subscribe(post => {
        this.post = post;
      });
    });
  }
  openDeleteDialog(post: Post) {
    const confirm = {
      title: 'Confirm to delete:',
      content:'Do you confirm to delete '+post.title,
      confirmAction: 'Confirm to delete',
    };
    let deleteId =post.id;
    console.log(deleteId);
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { dialog: confirm }
    });

    dialogRef
      .afterClosed()
      .subscribe(
        post => {
          if (post) {
              this.postService.deletePost(deleteId).subscribe(
              post => {
                this.router.navigate(['/blog/post-list/']);
              });
          }
      

        }
      );
  }
}
