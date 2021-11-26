import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {ExpenseTypeModel} from '../../../../model/api/catalog/expense/expenseTypeModel';

@Injectable()

export class ExpenseTypeService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public Get(): Observable<ExpenseTypeModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.expense.type.root;
    return this.http.get<ExpenseTypeModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: ExpenseTypeModel): Observable<ExpenseTypeModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.expense.type.root;
    return this.http.post<ExpenseTypeModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: ExpenseTypeModel): Observable<ExpenseTypeModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.expense.type.root;
    return this.http.put<ExpenseTypeModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: ExpenseTypeModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.expense.type.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
