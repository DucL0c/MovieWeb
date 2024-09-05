export interface MenuDto {
  menuName?: string;
  icon?: string;
  link?: string;
  childrens?: MenuDto[];
}
