import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { ApiOptions } from '../../../general/apiOptions';
import {TransactionDetailModel} from '../../../model/api/operation/transactionDetailModel';

@Injectable()

export class TransactionDetailService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public GetFiltered(transactionId: number): Observable<TransactionDetailModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.operation.transactionDetail.getFiltered
      .replace('{0}', transactionId.toString());
    return this.http.get<TransactionDetailModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: TransactionDetailModel): Observable<TransactionDetailModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.operation.transactionDetail.root;
    return this.http.post<TransactionDetailModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: TransactionDetailModel): Observable<TransactionDetailModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.operation.transactionDetail.root;
    return this.http.put<TransactionDetailModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: TransactionDetailModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.operation.transactionDetail.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
