import{ Component, OnInit, Output, EventEmitter }  from '@angular/core';
import { OpenIdConnectService } from '../../../shared/oidc/open-id-connect.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {

  @Output() toggleSidenav = new EventEmitter<void>();
  constructor(
    public openIdConnectService: OpenIdConnectService
  ) { }

  ngOnInit() {
  }

}