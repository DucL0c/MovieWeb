import { DataService } from './../core/services/data.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { AuthenService } from '../core/services/authen.service';
import { BreakpointObserver } from '@angular/cdk/layout';
import { NotificationService } from '../core/services/notification.service';
import { TranslateService } from '@ngx-translate/core';
import { SystemConstants } from '../core/common/system.constants';
import { MenuDto } from '../core/model/menu';
import { UrlConstants } from '../core/common/url.constants';
import { UltillityService } from '../core/services/ultillity.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent implements OnInit, AfterViewInit {
  account: any;
  user: { fullName: string; email: string; image: any; userName: string } = {
    fullName: 'John Doe',
    email: 'john.doe@example.com',
    image: null,
    userName: 'admin',
  };
  @ViewChild('sidenav') sidenav: MatSidenav | undefined;
  treeControl = new NestedTreeControl<MenuDto>((node) => node.childrens);
  dataSource = new MatTreeNestedDataSource<MenuDto>();
  @ViewChild('modelChagePassWord') dialogTemplate!: TemplateRef<any>;
  @ViewChild('configDeviceType') dialogTemplateConfig!: TemplateRef<any>;
  form!: FormGroup;

  constructor(
    public _authen: AuthenService,
    private utilityService: UltillityService,
    private elementRef: ElementRef,
    private observer: BreakpointObserver,
    private ref: ChangeDetectorRef,
    // private MatBreadcrumbService: MatBreadcrumbService,
    private dataService: DataService,
    private notificationService: NotificationService,
    public dialog: MatDialog,
    private translateService: TranslateService,
    private formBuilder: FormBuilder //private spinner: NgxSpinnerService
  ) {
    this.form = this.formBuilder.group({
      passwordOld: ['', Validators.compose([Validators.required])],
      passwordNew: ['', Validators.compose([Validators.required])],
      passwordNewRepeat: ['', Validators.compose([Validators.required])],
    });
    this.account = JSON.parse(
      localStorage.getItem(SystemConstants.CURRENT_USER) as string
    );
  }

  hasChild = (_: number, node: MenuDto) =>
    !!node.childrens && node.childrens.length > 0;

  ngOnInit(): void {
    // this.getUser();
    this.loadMenuUsers();
    // this.addListenerShowDialogConfig();
  }
  displayedColumns: string[] = [
    'position',
    'devTypeName',
    'devTypeCode',
    'getCardNo',
    'biometrics',
    'takePhoto',
    'qrCode',
    'syncTime',
    'timeLimit',
    'schedule',
    'canGetLog',
    'isElevator',
    'openDoor',
  ];
  addListenerShowDialogConfig() {
    document.body.addEventListener('keydown', function (event: any) {
      if (event.keyCode === 115) {
        let btnConfig = document.getElementById(
          'btnconfig'
        ) as HTMLButtonElement;
        btnConfig.click();
      }
    });
  }
  tDeviceTypeForm!: FormGroup;
  createDeviceTypeForm() {
    this.tDeviceTypeForm = this.formBuilder.group({
      tDeviceTypeForm: this.formBuilder.array([]),
    });
  }
  openDialogConfig() {
    this.dialog.closeAll();
    this.createDeviceTypeForm();
    this.loadAllTDeviceTypes();
    const dialogRef = this.dialog.open(this.dialogTemplateConfig, {
      width: '1600px',
      autoFocus: false,
    });
    dialogRef.disableClose = true;
  }
  dataSource2 = new MatTableDataSource<any>();
  loadAllTDeviceTypes() {
    // this.spinner.show();
    // this.dataService.get('tdevicetype/getall').subscribe(
    //   (data: any) => {
    //     this.tDeviceTypeForm = this.formBuilder.group({
    //       tDeviceTypeForm: this.formBuilder.array(
    //         data.map((val: any) =>
    //           this.formBuilder.group({
    //             devTypeId: new FormControl(val.devTypeId),
    //             devTypeName: new FormControl(val.devTypeName),
    //             devTypeCode: new FormControl(val.devTypeCode),
    //             getCardNo: new FormControl(val.getCardNo),
    //             biometrics: new FormControl(val.biometrics),
    //             takePhoto: new FormControl(val.takePhoto),
    //             qrCode: new FormControl(val.qrCode),
    //             syncTime: new FormControl(val.syncTime),
    //             timeLimit: new FormControl(val.timeLimit),
    //             schedule: new FormControl(val.schedule),
    //             canGetLog: new FormControl(val.canGetLog),
    //             isElevator: new FormControl(val.isElevator),
    //             openDoor: new FormControl(val.openDoor),
    //           })
    //         )
    //       ),
    //     });
    //     this.dataSource2 = new MatTableDataSource(
    //       (this.tDeviceTypeForm.get('tDeviceTypeForm') as FormArray).controls
    //     );
    //     this.spinner.hide();
    //   },
    //   (err: any) => {
    //     this.translateService
    //       .get('messageSystem.loadFail')
    //       .subscribe((data) => (MessageConstants.GET_FAILSE_MSG = data));
    //     this.notificationService.printErrorMessage(
    //       MessageConstants.GET_FAILSE_MSG
    //     );
    //     this.spinner.hide();
    //   }
    // );
  }
  saveConfig() {
    // this.spinner.show();
    // let devTypes = this.tDeviceTypeForm.value.tDeviceTypeForm;
    // this.dataService.put('tdeviceType/configdevicetype', devTypes).subscribe(
    //   (data: any) => {
    //     this.notificationService.printSuccessMessage('Cấu hình thành công');
    //     this.loadAllTDeviceTypes();
    //   },
    //   (err: any) => {
    //     this.notificationService.printErrorMessage(err.error.message);
    //     this.spinner.hide();
    //   }
    // );
  }

  loadMenuUsers() {
    const user = JSON.parse(
      localStorage.getItem(SystemConstants.CURRENT_USER)!
    );
    var id = user.id;
    this.dataService
      .get('SystemRole/gettreeviewbyuser?userId=' + id)
      .subscribe((data: any) => {
        this.dataSource.data = data;
        console.log(data);
      });
    // this.dataService
    //   .get('approles/gettreeviewbyuser?userId=' + user.id)
    //   .subscribe((data: any) => {
    //     this.dataSource.data = data;
    //   });
  }

  logOut() {
    // localStorage.removeItem(SystemConstants.CURRENT_USER);
    // localStorage.removeItem(SystemConstants.CURRENT_USER_ROLE);
    // localStorage.removeItem(SystemConstants.USERS_PIPE);
    // localStorage.removeItem(SystemConstants.USER_MENUS);
    // localStorage.removeItem(SystemConstants.DEP_ID_LIST);
    // this.utilityService.navigate(UrlConstants.LOGIN);
    localStorage.removeItem(SystemConstants.CURRENT_USER);
    this.utilityService.navigate(UrlConstants.LOGIN);
  }

  getUser() {
    let current = localStorage.getItem(SystemConstants.CURRENT_USER);
    //this.user = JSON.parse(current!);
  }

  getlistUser() {
    // this.dataService.get('appusers/getall').subscribe((data: any) => {
    //   localStorage.removeItem(SystemConstants.USERS_PIPE);
    //   localStorage.setItem(SystemConstants.USERS_PIPE, JSON.stringify(data));
    // });
  }

  ngAfterViewInit() {
    this.observer.observe(['(max-width:800px)']).subscribe((res) => {
      if (res.matches) {
        if (this.sidenav) {
          this.sidenav.mode = 'over';
          this.sidenav.close();
        }
      } else {
        if (this.sidenav) {
          this.sidenav.mode = 'side';
          this.sidenav.open();
        }
      }
    });
    this.ref.detectChanges();
  }

  //chage Password
  openModelChaglePassword() {
    const dialogRef = this.dialog.open(this.dialogTemplate, {
      width: '600px',
      autoFocus: false,
    });
    dialogRef.disableClose = true;
  }
  closeModelChangepassword() {
    this.dialog.closeAll();
    this.form.reset();
  }

  change() {
    // const oldpass = this.form.controls['passwordOld'].value;
    // const newpass = this.form.controls['passwordNew'].value;
    // const newPassRepeat = this.form.controls['passwordNewRepeat'].value;
    // if (!this.form.invalid) {
    //   // check mật khẩu cũ có đúng mật khẩu đang đăng nhập không
    //   this.dataService
    //     .get(
    //       'Accounts/Checkpassword?userName=' +
    //         JSON.parse(localStorage.CURRENT_USER).userName +
    //         '&pass=' +
    //         oldpass
    //     )
    //     .subscribe((respon: any) => {
    //       var check = respon;
    //       if (check == false) {
    //         let error!: string;
    //         this.translateService
    //           .get('changePassWord.errorPassOld')
    //           .subscribe((mes) => (error = mes));
    //         this.notificationService.printErrorMessage(error);
    //       }
    //       return;
    //     });
    //   //end check mật khẩu
    //   if (oldpass == newpass) {
    //     let error!: string;
    //     this.translateService
    //       .get('changePassWord.passOldCheck')
    //       .subscribe((mes) => (error = mes));
    //     this.notificationService.printErrorMessage(error);
    //     return;
    //   }
    //   if (newpass != newPassRepeat) {
    //     let error!: string;
    //     this.translateService
    //       .get('changePassWord.passNewCheck')
    //       .subscribe((mes) => (error = mes));
    //     this.notificationService.printErrorMessage(error);
    //     return;
    //   }
    //   //Đổi mật khẩu
    //   let param = {
    //     id: JSON.parse(localStorage.CURRENT_USER).id,
    //     passwordHash: newpass,
    //   };
    //   this.dataService.put('AppUsers/UpdateFromUser', param).subscribe(
    //     (respon: any) => {
    //       let success!: string;
    //       this.translateService
    //         .get('changePassWord.changePassSuccess')
    //         .subscribe((mes) => (success = mes));
    //       this.notificationService.printSuccessMessage(success);
    //       this.dialog.closeAll();
    //       this.form.reset();
    //       this.utilityService.navigate(UrlConstants.LOGIN);
    //     },
    //     (err) => {
    //       err.error;
    //     }
    //   );
    //   //end đổi mk
    // }
  }

  get getValidForm(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }
}
