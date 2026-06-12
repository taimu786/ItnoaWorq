export interface ApiResponse<T> {
  data: T;
  message?: string;
}

export interface PaginationParams {
  page?: number;
  pageSize?: number;
}