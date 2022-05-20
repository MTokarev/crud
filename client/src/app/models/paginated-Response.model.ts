export interface PaginatedResponse<T> {
  pageIndex: number;
  pageSize: number;
  pageSizeMaxAllowed: number;
  totalItems: number;
  totalPages: number;
  itemsReturned: number;
  data: T[];
}