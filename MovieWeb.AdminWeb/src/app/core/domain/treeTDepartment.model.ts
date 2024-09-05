export class TreeTDepartment {
  id!: number;
  name!: string;
  childrens?: TreeTDepartment[];
  constructor(id: number, name: string, childrens: TreeTDepartment[]) {
    this.id = id;
    this.name = name;
    this.childrens = childrens;
  }
}