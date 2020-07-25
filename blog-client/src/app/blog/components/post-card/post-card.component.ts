import { Component, OnInit, Input } from '@angular/core';
import { Post } from '../../models/post';
import { OpenIdConnectService } from '../../../shared/oidc/open-id-connect.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { PostService } from '../../services/post.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss']
})
export class PostCardComponent implements OnInit {
  @Input() post: Post;
  constructor(
    private postService: PostService,
    private router: Router,
    public openIdConnectService: OpenIdConnectService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDeleteDialog(post: Post) {
    const confirm = {
      title: 'Confirm to delete:',
      content:'Do you confirm to delete \''+post.title+'\'',
      confirmAction: 'Delete',
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
                // this.router.navigate(['/blog/post-list/']);
                // const deletedContrat = this.post.find(x => x.id === post.id);
                // this.post.splice(this.post.indexOf(deletedContrat), 1);
              });
          }
        }
      );
  }
  
}
