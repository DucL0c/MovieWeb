import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthenService } from '../../core/services/authen.service';
import { Router } from '@angular/router';
import { MatSort } from '@angular/material/sort';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { DatePipe } from '@angular/common';
import { NotificationService } from '../../core/services/notification.service';
import { TranslateService } from '@ngx-translate/core';
import { interval } from 'rxjs';
import { SystemConstants } from '../../core/common/system.constants';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;
  pageSizeOptions: number[] = [3, 10, 50, 100];
  pageSize = this.pageSizeOptions[0];
  page = 0;
  pageSizeParking = this.pageSizeOptions[0];
  pageParking = 0;
  account!: any | undefined;
  submitted: boolean = false;
  appUsers: any;
  // appUser: any;
  form!: FormGroup;
  dataSource = new MatTableDataSource<any>();
  dataSourceParking = new MatTableDataSource<any>();
  // dateFrom = this.datePipe.transform(
  //   new Date().setHours(0, 0, 0, 0),
  //   'yyyy-MM-ddTHH:mm:ss'
  // );
  // dateTo = this.datePipe.transform(
  //   new Date().setHours(23, 59, 58, 0),
  //   'yyyy-MM-ddTHH:mm:ss'
  // );
  displayedColumns: string[] = [
    'position',
    'emId',
    'emIdentityNumber',
    'emName',
    'depName',
    'timeIn',
    'timeOut',
  ];
  displayedParkingColumns: string[] = [
    'position',
    'emName',
    'cardNO-Number',
    'vehicleType',
    'ticketType',
    'registerNumber',
    'plateNumber',
    'ioStatus',
    'ioTime',
    'price',
  ];
  totalIn: number = 0;
  totalOut: number = 0;
  totalInParking: number = 0;
  totalOutParking: number = 0;
  user: number = 0;
  finger: number = 0;
  face: number = 0;
  card: number = 0;
  device: number = 0;
  late: number = 0;
  early: number = 0;
  absent: number = 0;
  overTime: number = 0;
  data: any;
  dataParking: any;
  totalRow: number = 0;
  totalRowParking: number = 0;
  showList = true;
  appGroups: any;
  disableButton = true;
  StateImage = false;
  constructor(
    public _authen: AuthenService,
    private notificationService: NotificationService,
    //private dataService: DataService,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private translateService: TranslateService
  ) {
    this.account = JSON.parse(localStorage.getItem('user') as string);
    this.datePipe = new DatePipe('en-US'); // Initialize the datePipe property

    this.form = this.formBuilder.group({
      id: '',
      //   groups: [],
      eM_ID: null,
      userName: [
        '',
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(50),
        ],
      ],
      email: ['', [Validators.email]],
      password: ['', [Validators.minLength(0), Validators.maxLength(50)]],
      image: '',
      //confirmPassword: ['', Validators.required],
      // acceptTerms: [false, Validators.requiredTrue]
    });
  }

  ngOnInit(): void {
    if (this.account == undefined) {
      this.StateImage = true;
    }
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
  changeListener($event: any): void {
    this.readThis($event.target);
  }

  readThis(inputValue: any): void {
    var file: File = inputValue.files[0];
    var myReader: FileReader = new FileReader();

    myReader.onloadend = (e) => {
      this.account.image = myReader.result?.slice(23);
    };
    myReader.readAsDataURL(file);
  }
  //change paging
  onChangePage(pe: PageEvent) {
    this.page = pe.pageIndex;
    this.pageSize = pe.pageSize;
    //this.loadData();
  }
  //change paging
  onChangePageParking(pe: PageEvent) {
    this.pageParking = pe.pageIndex;
    this.pageSizeParking = pe.pageSize;
    //this.loadDataParking();
  }
}
