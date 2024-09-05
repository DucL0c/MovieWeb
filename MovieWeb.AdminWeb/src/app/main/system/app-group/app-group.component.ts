import { TreeTDepartment } from './../../../core/domain/treeTDepartment.model';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
  MatTreeNestedDataSource,
} from '@angular/material/tree';
import { SystemConstants } from '../../../core/common/system.constants';
import { RoleNode } from '../../../core/domain/treeRole.model';
import { MessageConstants } from '../../../core/common/message.constants';
import { NotificationService } from '../../../core/services/notification.service';
import { UserRoles } from '../../../core/common/userRole.pipe';
import { DataService } from '../../../core/services/data.service';
import { NgxSpinnerService } from 'ngx-spinner';
const user = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER)!);

interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
  id: string;
}

interface FlatNodeTemplate {
  expandable: boolean;
  name: string;
  level: number;
  id: number;
}
@Component({
  selector: 'app-app-group',
  templateUrl: './app-group.component.html',
  styleUrl: './app-group.component.css',
})
export class AppGroupComponent implements OnInit {
  displayedColumns: string[] = [
    'select',
    'position',
    'groupCode',
    'name',
    'createdDate',
    'createdBy',
    'action',
  ];
  dataSource = new MatTableDataSource<any>();
  preventAbuse = false;
  page = 0;
  keyword: string = '';
  totalRow: number = 0;
  totalPage: number = 0;
  roleParents: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('dialog') dialogTemplate!: TemplateRef<any>;
  selection = new SelectionModel<any>(true, []);
  groupForm!: FormGroup;
  title!: string;
  action!: string;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageSize = this.pageSizeOptions[0];
  isAllChecked = false;
  allComplete: boolean = false;
  roleGroups: any;
  private _transformer = (node: RoleNode, level: number) => {
    return {
      expandable: !!node.childrens && node.childrens.length > 0,
      name: node.description,
      level: level,
      id: node.id,
    };
  };
  treeControl = new FlatTreeControl<ExampleFlatNode>(
    (node) => node.level,
    (node) => node.expandable
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    (node) => node.level,
    (node) => node.expandable,
    (node) => node.childrens
  );
  treeRoles = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
  checklistSelection = new SelectionModel<ExampleFlatNode>(true /* multiple */);

  constructor(
    private spinner: NgxSpinnerService,
    //private pagin: PaginatorCustomService,
    private dataService: DataService,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private dialog: MatDialog,
    private formBuilder: FormBuilder
  ) {
    this.groupForm = this.formBuilder.group({
      id: 0,
      groupCode: [
        '',
        Validators.compose([Validators.required, Validators.maxLength(50)]),
      ],
      name: [
        '',
        Validators.compose([Validators.required, Validators.maxLength(100)]),
      ],
      createdDate: '',
      createdBy: '',
      updatedDate: '',
      updatedBy: '',
      isDeleted: '',
      roles: [],
    });
  }

  ngOnInit(): void {
    this.loadAllGroups();
    //this.loadAllTreeRoles();
  }

  // Duc Anh comment
  // descendantsAllSelected(node: ExampleFlatNode): boolean {
  //   const descendants = this.treeControl.getDescendants(node);
  //   const descAllSelected =
  //     descendants.length > 0 &&
  //     descendants.every((child: any) => {
  //       return this.checklistSelection.isSelected(child);
  //     });
  //   return descAllSelected;
  // }
  // Duc Anh them
  descendantsAllSelected(node: ExampleFlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected =
      descendants.length > 0 &&
      descendants.some((child: any) => {
        return this.checklistSelection.isSelected(child);
      });
    return descAllSelected;
  }

  todoItemSelectionToggle(node: ExampleFlatNode): void {
    this.checklistSelection.toggle(node);
    const descendants = this.treeControl.getDescendants(node);
    this.checklistSelection.isSelected(node)
      ? this.checklistSelection.select(...descendants)
      : this.checklistSelection.deselect(...descendants);

    // Force update for the parent
    descendants.forEach((child) => this.checklistSelection.isSelected(child));
    this.checkAllParentsSelection(node);
  }

  /* Checks all the parents when a leaf node is selected/unselected */
  checkAllParentsSelection(node: ExampleFlatNode): void {
    let parent: ExampleFlatNode | null = this.getParentNode(node);
    while (parent) {
      this.checkRootNodeSelection(parent);
      parent = this.getParentNode(parent);
    }
  }
  getLevel = (node: ExampleFlatNode) => node.level;

  /* Get the parent node of a node */
  getParentNode(node: ExampleFlatNode): ExampleFlatNode | null {
    const currentLevel = this.getLevel(node);

    if (currentLevel < 1) {
      return null;
    }

    const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;

    for (let i = startIndex; i >= 0; i--) {
      const currentNode = this.treeControl.dataNodes[i];

      if (this.getLevel(currentNode) < currentLevel) {
        return currentNode;
      }
    }
    return null;
  }

  /** Check root node checked state and change it accordingly */
  // Duc Anh comment
  // checkRootNodeSelection(node: ExampleFlatNode): void {
  //   const nodeSelected = this.checklistSelection.isSelected(node);
  //   const descendants = this.treeControl.getDescendants(node);
  //   const descAllSelected =
  //     descendants.length > 0 &&
  //     descendants.every(child => {
  //       return this.checklistSelection.isSelected(child);
  //     });
  //   if (nodeSelected && !descAllSelected) {
  //     this.checklistSelection.deselect(node);
  //   } else if (!nodeSelected && descAllSelected) {
  //     this.checklistSelection.select(node);
  //   }
  // }
  // Duc Anh them
  checkRootNodeSelection(node: ExampleFlatNode): void {
    const nodeSelected = this.checklistSelection.isSelected(node);
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected =
      descendants.length > 0 &&
      descendants.some((child) => {
        return this.checklistSelection.isSelected(child);
      });
    if (nodeSelected && !descAllSelected) {
      this.checklistSelection.deselect(node);
    } else if (!nodeSelected && descAllSelected) {
      this.checklistSelection.select(node);
    }
  }

  descendantsPartiallySelected(node: ExampleFlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const result = descendants.some((child) =>
      this.checklistSelection.isSelected(child)
    );
    return result && !this.descendantsAllSelected(node);
  }

  /** Toggle a leaf to-do item selection. Check all the parents to see if they changed */
  todoLeafItemSelectionToggle(node: ExampleFlatNode): void {
    this.checklistSelection.toggle(node);
    this.checkAllParentsSelection(node);
  }

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  openDialog(action: string, item?: any, config?: MatDialogConfig) {
    this.action = action;
    this.getAllDepartment();
    this.resetTreeChecked();
    if (action == 'create') {
      this.groupForm.reset();
      this.translateService
        .get('appGroupModule.createMessage')
        .subscribe((data) => (this.title = data));
    } else {
      this.translateService
        .get('appGroupModule.editMessage')
        .subscribe((data) => (this.title = data));
      this.groupForm.controls['id'].setValue(item.id);
      let itemFilter = this.dataSource.filteredData.filter(
        (x: any) => x.id == item.id
      )[0];
      this.groupForm.controls['name'].setValue(itemFilter.name);
      this.groupForm.controls['groupCode'].setValue(itemFilter.groupCode);
      this.groupForm.controls['createdDate'].setValue(itemFilter.createdDate);
      this.groupForm.controls['createdBy'].setValue(itemFilter.createdBy);
      this.groupForm.controls['updatedDate'].setValue(itemFilter.updatedDate);
      this.groupForm.controls['updatedBy'].setValue(itemFilter.updatedBy);
      this.getRoleByGroup(item.id);
      this.getDepByGroup(item.id);
    }
    const dialogRef = this.dialog.open(this.dialogTemplate, {
      width: '750px',
    });
    dialogRef.disableClose = true;
    dialogRef.afterClosed().subscribe((result) => {
      this.onReset();
    });
  }
  removeData() {
    let roleChecked: any[] = [];
    this.selection.selected.forEach((value: any) => {
      let id = value.id;
      roleChecked.push(id);
    });
    this.translateService
      .get('messageSystem.confirmDelete')
      .subscribe((data) => (MessageConstants.CONFIRM_DELETE_MSG = data));
    this.notificationService.printConfirmationDialog(
      MessageConstants.CONFIRM_DELETE_MSG,
      () => this.deleteItemConfirm(JSON.stringify(roleChecked))
    );
  }

  deleteItemConfirm(id: string) {
    console.log('oke');
    // this.preventAbuse = true;
    // this.spinner.show();
    // this.dataService
    //   .delete('appgroups/deletemulti', 'checkedList', id)
    //   .subscribe(
    //     (response) => {
    //       this.translateService
    //         .get('messageSystem.deleteSuccess')
    //         .subscribe((data) => (MessageConstants.DELETED_OK_MSG = data));
    //       this.notificationService.printSuccessMessage(
    //         MessageConstants.DELETED_OK_MSG
    //       );
    //       this.selection.clear();
    //       this.loadAllGroups();
    //       this.spinner.hide();
    //       this.preventAbuse = false;
    //     },
    //     (err) => {
    //       this.translateService
    //         .get('messageSystem.deletefail')
    //         .subscribe((data) => (MessageConstants.DELETE_FAILSE_MSG = data));
    //       this.notificationService.printErrorMessage(
    //         MessageConstants.DELETE_FAILSE_MSG
    //       );
    //       this.spinner.hide();
    //       this.preventAbuse = false;
    //     }
    //   );
  }
  applyFilter(event: Event) {
    this.keyword = (event.target as HTMLInputElement).value;
    this.page = 0;
    this.loadAllGroups();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  addData() {
    let group = {
      name: this.groupForm.controls['name'].value,
      groupCode: this.groupForm.controls['groupCode'].value,
    };
    console.log(group);
    // if (this.groupForm.invalid) {
    //   return;
    // }
    // let depSelecteds: any = [];
    // this.treeControl2.dataNodes
    //   .filter((x) => x.level == 1)
    //   .forEach((element: any) => {
    //     if (
    //       this.checklistSelection2.selected
    //         .filter((x) => x.level == 1)
    //         .indexOf(element) >= 0
    //     ) {
    //       depSelecteds.push(element.id);
    //     }
    //   });
    // if (depSelecteds.length == 0) {
    //   let mess!: string;
    //   this.translateService
    //     .get('appGroupModule.chooseDepRequireMessage')
    //     .subscribe((data) => (mess = data));
    //   this.notificationService.printErrorMessage(mess);
    //   return;
    // }
    // let depGroupStr = JSON.stringify(depSelecteds);
    // this.preventAbuse = true;
    // let roles = this.getListRoleId();
    // if (this.action == 'create') {
    //   this.spinner.show();
    //   let group = {
    //     name: this.groupForm.controls['name'].value,
    //     groupCode: this.groupForm.controls['groupCode'].value,
    //     createdBy: user.id,
    //     roles: roles,
    //     depGroupStr: depGroupStr,
    //   };
    //   this.dataService.post('SystemGroup/Create', group).subscribe(
    //     (data) => {
    //       let message!: string;
    //       this.translateService
    //         .get('messageSystem.createSuccess')
    //         .subscribe((mes) => (message = mes));
    //       this.notificationService.printSuccessMessage(message);
    //       this.loadAllGroups();
    //       this.spinner.hide();
    //       this.preventAbuse = false;
    //       console.log(data);
    //     },
    //     (err: any) => {
    //       this.notificationService.printErrorMessage(err.error.message);
    //       this.spinner.hide();
    //       this.preventAbuse = false;
    //     }
    //   );
    // } else if (this.action == 'edit') {
    //   this.spinner.show();
    //   let group = {
    //     name: this.groupForm.controls['name'].value,
    //     groupCode: this.groupForm.controls['groupCode'].value,
    //     id: this.groupForm.controls['id'].value,
    //     updatedBy: user.id,
    //     roles: roles,
    //     depGroupStr: depGroupStr,
    //   };
    //   this.dataService.put('SystemGroup/Update', group).subscribe(
    //     (data) => {
    //       let message!: string;
    //       this.translateService
    //         .get('messageSystem.updateSuccess')
    //         .subscribe((mes) => (message = mes));
    //       this.notificationService.printSuccessMessage(message);
    //       this.loadAllGroups();
    //       this.spinner.hide();
    //       this.preventAbuse = false;
    //     },
    //     (err: any) => {
    //       this.notificationService.printErrorMessage(err.error.message);
    //       this.spinner.hide();
    //       this.preventAbuse = false;
    //     }
    //   );
    // }
  }

  getListRoleId(): string[] {
    let strRoles: any[] = [];
    if (this.checklistSelection.selected.length > 0) {
      this.checklistSelection.selected.forEach((value: any) => {
        let roleId = { id: '' };
        roleId.id = value.id;
        strRoles.push(roleId);
      });
    }
    return strRoles;
  }

  getRoleByGroup(groupId: any) {
    // this.dataService
    //   .get('appRoles/getlistbygroupid?groupId=' + groupId)
    //   .subscribe((data: any) => {
    //     this.roleGroups = data;
    //     this.setCheckedRole();
    //   });
  }

  resetSelection() {
    this.treeControl.dataNodes.forEach((element: any) => {
      this.checklistSelection.deselect(element);
    });
  }

  setCheckedRole() {
    //reset checklistSelection
    this.resetSelection();

    //check nếu có tồn tại quyền trong group thì add vào checklistSelection
    this.roleGroups.forEach((value: any) => {
      let select = this.treeControl.dataNodes.filter(
        (x: any) => x.id == value.id
      )[0];
      //this.todoLeafItemSelectionToggle(select);
      this.checklistSelection.toggle(select);
    });

    this.roleGroups.forEach((element: any) => {
      let select = this.treeControl.dataNodes.filter(
        (x: any) => x.id == element.id
      )[0];
      this.checkAllParentsSelection(select);
    });
  }

  loadAllGroups() {
    this.preventAbuse = true;
    this.dataService
      .get(
        'SystemGroup/Getallbypaging?page=' +
          this.page +
          '&pageSize=' +
          this.pageSize +
          '&keyword=' +
          this.keyword
      )
      .subscribe(
        (data: any) => {
          this.dataSource = new MatTableDataSource(data.items);
          this.dataSource.sort = this.sort;
          this.totalRow = data.totalCount;
          this.preventAbuse = false;
          console.log(data);
        },
        (err) => {
          this.translateService
            .get('messageSystem.loadFail')
            .subscribe((data) => (MessageConstants.GET_FAILSE_MSG = data));
          this.notificationService.printErrorMessage(
            MessageConstants.GET_FAILSE_MSG
          );
          this.preventAbuse = false;
        }
      );
  }

  loadAllTreeRoles() {
    this.dataService.get('appRoles/gettreeroles').subscribe(
      (data: any) => {
        this.treeRoles.data = data;
        this.dataTreeSource2.data = data;
      },
      (err) => {}
    );
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${
      row.position + 1
    }`;
  }

  ngAfterViewInit() {
    // this.paginator._intl.itemsPerPageLabel = this.pagin.setLable;
    // this.paginator._intl.firstPageLabel = this.pagin.firstButton;
    // this.paginator._intl.nextPageLabel = this.pagin.nextButton;
    // this.paginator._intl.lastPageLabel = this.pagin.lastButton;
    // this.paginator._intl.previousPageLabel = this.pagin.preButton;
  }

  onChangePage(pe: PageEvent) {
    this.page = pe.pageIndex;
    this.pageSize = pe.pageSize;
    this.loadAllGroups();
  }

  get getValidForm(): { [key: string]: AbstractControl } {
    return this.groupForm.controls;
  }

  onReset() {
    this.action == '';
    this.resetSelection();
    this.groupForm.reset();
  }
  deps: any[] = [];

  getAllDepartment() {
    // this.preventAbuse = true;
    // this.dataService.get('AppGroups/gettreedepartment').subscribe(
    //   (data: any) => {
    //     this.deps = data;
    //     this.preventAbuse = false;
    //     let allDep!: string;
    //     this.translateService
    //       .get('appGroupModule.allDep')
    //       .subscribe((data) => (allDep = data));
    //     let tree: TreeTDepartment[] = [new TreeTDepartment(0, allDep, data)];
    //     this.dataTreeSource2.data = tree;
    //   },
    //   (err: any) => {
    //     this.translateService
    //       .get('messageSystem.loadFail')
    //       .subscribe((data) => (MessageConstants.GET_FAILSE_MSG = data));
    //     this.notificationService.printErrorMessage(
    //       MessageConstants.GET_FAILSE_MSG
    //     );
    //     this.preventAbuse = false;
    //   }
    // );
  }

  private _transformer2 = (node: TreeTDepartment, level: number) => {
    return {
      expandable: !!node.childrens && node.childrens.length > 0,
      name: node.name,
      level: level,
      id: node.id,
    };
  };
  treeControl2 = new FlatTreeControl<FlatNodeTemplate>(
    (node) => node.level,
    (node) => node.expandable
  );
  treeFlattener2 = new MatTreeFlattener(
    this._transformer2,
    (node) => node.level,
    (node) => node.expandable,
    (node) => node.childrens
  );

  dataTreeSource2 = new MatTreeFlatDataSource(
    this.treeControl2,
    this.treeFlattener2
  );

  /** The selection for checklist */
  checklistSelection2 = new SelectionModel<FlatNodeTemplate>(
    true /* multiple */
  );
  hasChild2 = (_: number, node: FlatNodeTemplate) => node.expandable;

  /** tất cả children checked => checked parent */
  descendantsAllSelected2(node: FlatNodeTemplate): boolean {
    const descendants = this.treeControl2.getDescendants(node);
    return descendants.every((child) =>
      this.checklistSelection2.isSelected(child)
    );
  }

  /** children ckecked (ngoại trừ tất cả children checked) => [indeterminate] parent */
  descendantsPartiallySelected2(node: FlatNodeTemplate): boolean {
    const descendants = this.treeControl2.getDescendants(node);
    const result = descendants.some((child) =>
      this.checklistSelection2.isSelected(child)
    );
    return result && !this.descendantsAllSelected2(node);
  }

  /** checked parent => checked tất cả children */
  todoItemSelectionToggle2(node: FlatNodeTemplate): void {
    this.checklistSelection2.toggle(node);
    const descendants = this.treeControl2.getDescendants(node);
    this.checklistSelection2.isSelected(node)
      ? this.checklistSelection2.select(...descendants)
      : this.checklistSelection2.deselect(...descendants);
  }

  /** thay đổi check của children */
  todoLeafItemSelectionToggle2(node: FlatNodeTemplate): void {
    this.checklistSelection2.toggle(node);
    this.checkAllParentsSelection2(node);
  }

  /* vòng lặp tìm các parent và kiểm tra điều kiện để checked parent */
  checkAllParentsSelection2(node: FlatNodeTemplate): void {
    let parent: FlatNodeTemplate | null = this.getParentNode2(node);
    while (parent) {
      this.checkRootNodeSelection2(parent);
      parent = this.getParentNode2(parent);
    }
  }

  getParentNode2(node: FlatNodeTemplate): FlatNodeTemplate | null {
    const currentLevel = this.getLevel2(node);
    if (currentLevel < 1) {
      return null;
    }
    const startIndex = this.treeControl2.dataNodes.indexOf(node) - 1;
    for (let i = startIndex; i >= 0; i--) {
      const currentNode = this.treeControl2.dataNodes[i];
      if (this.getLevel2(currentNode) < currentLevel) {
        return currentNode;
      }
    }
    return null;
  }

  getLevel2 = (node: FlatNodeTemplate) => node.level;

  /** check để checked parent hoặc không */
  checkRootNodeSelection2(node: FlatNodeTemplate): void {
    // const nodeSelected = this.checklistSelection2.isSelected(node);
    // const descendants = this.treeControl2.getDescendants(node);
    // const descAllSelected =
    //   descendants.length > 0 &&
    //   descendants.every((child) => {
    //     return this.checklistSelection2.isSelected(child);
    //   });
    // if (nodeSelected && !descAllSelected) {
    //   /** parent đang checked và tất cả children không checked thì bỏ checked parent */ this.checklistSelection2.deselect(
    //     node
    //   );
    // } else if (!nodeSelected && descAllSelected) {
    //   /** parent không checked và tất cả children đang checked thì checked parent */ this.checklistSelection2.select(
    //     node
    //   );
    // } else {
    // }
  }
  tabGroupChange($event: any) {
    // let tab2Title!: string;
    // this.translateService
    //   .get('appGroupModule.tab2Title')
    //   .subscribe((data) => (tab2Title = data));
    // if (tab2Title == $event.tab.textLabel) {
    //   this.treeControl2.expandAll();
    // }
  }
  resetTreeChecked() {
    // this.treeControl2.dataNodes.forEach((element: any) => {
    //   this.checklistSelection2.deselect(element);
    // });
  }
  getDepByGroup(groupId: any) {
    // this.dataService
    //   .get('AppGroups/getdepList/' + groupId)
    //   .subscribe((data: any) => {
    //     this.treeControl2.dataNodes
    //       .filter((x) => x.level == 1)
    //       .forEach((element: any) => {
    //         if (data.indexOf(element.id) >= 0) {
    //           this.checklistSelection2.select(element);
    //           this.checkAllParentsSelection2(element);
    //         }
    //       });
    //   });
  }
}
