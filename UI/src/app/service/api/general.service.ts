import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { ApiOptions } from '../../general/apiOptions';
import {MonthModel} from '../../model/api/general/monthModel';
import {DocTypeModel} from '../../model/api/general/docTypeModel';
import {TransactionTypeModel} from '../../model/api/operation/transactionTypeModel';

@Injectable()

export class GeneralService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public GetMonths(): Observable<MonthModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.general.months;
    return this.http.get<MonthModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public GetDocTypes(): Observable<DocTypeModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.general.docType;
    return this.http.get<DocTypeModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public GetTransactionType(): Observable<TransactionTypeModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.general.transactionType;
    return this.http.get<TransactionTypeModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
