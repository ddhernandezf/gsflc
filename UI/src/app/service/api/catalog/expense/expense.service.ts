import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {ExpenseGroupedModel} from '../../../../model/api/catalog/expense/expenseGroupedModel';
import {ExpenseModel} from '../../../../model/api/catalog/expense/expenseModel';

@Injectable()

export class ExpenseService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public GetGrouped(): Observable<ExpenseGroupedModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.expense.getGrouped;
    return this.http.get<ExpenseGroupedModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Get(): Observable<ExpenseModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.expense.root;
    return this.http.get<ExpenseModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: ExpenseModel): Observable<ExpenseModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.expense.root;
    return this.http.post<ExpenseModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: ExpenseModel): Observable<ExpenseModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.expense.root;
    return this.http.put<ExpenseModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: ExpenseModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.expense.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
