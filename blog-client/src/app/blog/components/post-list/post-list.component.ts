import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/post.service';
import { PostParameters } from '../../models/post-parameters';
import { PageMeta } from '../../../shared/models/page-meta';
import { ResultWithLinks } from '../../../shared/models/result-with-links';
import { Post } from '../../models/post';
import { OpenIdConnectService } from '../../../shared/oidc/open-id-connect.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit {

  posts: Post[];
  pageMeta: PageMeta;
  postParameter = new PostParameters({ orderBy: 'id desc', pageSize: 10, pageIndex: 0 });
  searchKeyUp = new Subject<string>();
  constructor(private postService: PostService,
    private dialog: MatDialog,
    private openIdConnectService: OpenIdConnectService) { 
      const subscription = this.searchKeyUp.pipe(
        debounceTime(500),
        distinctUntilChanged()
      ).subscribe(() => {
        this.applyFilter(this.postParameter.title);
      });
    }
  ngOnInit() {
    this.posts = [];
    this.getPosts();
  }

  applyFilter(filterValue: string) {
    console.log(filterValue);
    
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.postParameter.title = filterValue;
    this.getPostswithFilter();
  }

  getPostswithFilter() {
    this.postService.getPagedPosts(this.postParameter).subscribe(resp => {
      this.pageMeta = JSON.parse(resp.headers.get('X-Pagination')) as PageMeta;
      const result = {...resp.body} as ResultWithLinks<Post>;
      this.posts = result.value;
      console.log(this.posts);
      console.log(this.postParameter.title);
      
    });
  }

  getPosts() {
    this.postService.getPagedPosts(this.postParameter).subscribe(resp => {
      this.pageMeta = JSON.parse(resp.headers.get('X-Pagination')) as PageMeta;
      const result = {...resp.body} as ResultWithLinks<Post>;
      this.posts = this.posts.concat(result.value);
    });
  }

  onScroll() {
    this.postParameter.pageIndex++;
    if (this.postParameter.pageIndex < this.pageMeta.pageCount) {
      this.getPosts();
    }
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
                const deletedContrat = this.posts.find(x => x.id === deleteId);           
                this.posts.splice(this.posts.indexOf(deletedContrat), 1);
              });
          }
        }
      );
  }

}
