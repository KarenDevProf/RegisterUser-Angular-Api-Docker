
export interface ResponseObjectModel {
  hasError: boolean;
  message: string | null;
  errorCode: string | null;
  data: any[];
}