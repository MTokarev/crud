export class PageParams {
  public pageSize: number;
  public pageIndex: number;

  constructor(pageSize: number, pageIndex: number) {  
    this.pageSize = pageSize,
    this.pageIndex =pageIndex
  }
}