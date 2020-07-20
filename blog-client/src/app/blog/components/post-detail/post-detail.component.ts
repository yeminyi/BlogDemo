import { Component, OnInit } from '@angular/core';
import { Post } from '../../models/post';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../../services/post.service';
import { OpenIdConnectService } from '../../../shared/oidc/open-id-connect.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss']
})
export class PostDetailComponent implements OnInit {
  post: Post;

  constructor(private route: ActivatedRoute,
    private postService: PostService,
    private openIdConnectService: OpenIdConnectService) { }

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
}
