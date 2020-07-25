import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
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
  @Output() launchDeleteDailog = new EventEmitter<void>();
  constructor(
    private postService: PostService,
    private router: Router,
    public openIdConnectService: OpenIdConnectService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDeleteDialog(ev: Event) {
    ev.preventDefault();
    this.launchDeleteDailog.emit();
  }
  
}
