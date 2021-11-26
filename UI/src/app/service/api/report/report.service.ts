import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { ApiOptions } from '../../../general/apiOptions';
import {BalanceModel} from '../../../model/api/report/balanceModel';
import {BalanceFilter} from '../../../model/forms/report/balanceFilter';

@Injectable()

export class ReportService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public GetBalanceData(option: string, filter: BalanceFilter): Observable<BalanceModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.report.balance
      .replace('{0}', option);
    return this.http.post<BalanceModel>(endpoint, filter, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
