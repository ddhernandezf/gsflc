import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { ApiOptions } from '../../../general/apiOptions';
import {TransactionGridModel} from '../../../model/api/operation/transactionGridModel';
import {TransactionModel} from '../../../model/api/operation/transactionModel';

@Injectable()

export class TransactionService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public GetFiltered(year: number, month: number, vehicleId: number): Observable<TransactionGridModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.operation.transaction.getFiltered
      .replace('{0}', year.toString())
      .replace('{1}', month.toString())
      .replace('{2}', vehicleId.toString());
    return this.http.get<TransactionGridModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: TransactionModel): Observable<TransactionModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.operation.transaction.root;
    return this.http.post<TransactionModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: TransactionModel): Observable<TransactionModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.operation.transaction.root;
    return this.http.put<TransactionModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: TransactionGridModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.operation.transaction.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
