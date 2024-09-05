import { Component, OnInit } from '@angular/core';
import { AuthenService } from '../core/services/authen.service';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UrlConstants } from '../core/common/url.constants';
import { MessageConstants } from '../core/common/message.constants';
import { SystemConstants } from '../core/common/system.constants';
import { NotificationService } from '../core/services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  model: { username: string; password: string } = {
    username: '',
    password: '',
  };
  returlUrl: string | undefined;
  loginForm: any;
  preventAbuse = false;

  constructor(
    private authenService: AuthenService,
    private notificationService: NotificationService,
    private router: Router,
    private translate: TranslateService,
    private httpClient: HttpClient
  ) {}

  login() {
    if (!this.validate()) {
      return;
    }
    this.preventAbuse = true;
    this.authenService.login(this.model).subscribe(
      (data) => {
        this.router.navigate([UrlConstants.HOME]);
        this.preventAbuse = false;
      },
      (err) => {
        if (err.status === 401) {
          this.translate
            .get('messageSystem.incorrectAccount')
            .subscribe(
              (mes) => (MessageConstants.SYS_ERROR_LOGIN_FAILSE = mes)
            );
          this.notificationService.printErrorMessage(
            MessageConstants.SYS_ERROR_LOGIN_FAILSE
          );
        } else {
          this.translate
            .get('messageSystem.serverNotConnect')
            .subscribe(
              (mes) => (MessageConstants.SYS_ERROR_LOGIN_FAILSE = mes)
            );
          this.notificationService.printErrorMessage(
            MessageConstants.SYS_ERROR_LOGIN_FAILSE
          );
        }
        this.preventAbuse = false;
      }
    );
  }

  validate(): boolean {
    if (this.model.username == undefined) {
      return false;
    } else if (this.model.password == undefined) {
      return false;
    } else return true;
  }

  ngOnInit(): void {
    if (this.authenService.isLoggedIn()) {
      this.router.navigate([UrlConstants.HOME]);
    }
  }
}
