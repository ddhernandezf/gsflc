import { Injectable } from '@angular/core';
import {HttpClient, HttpEvent, HttpRequest, HttpResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ApiOptions } from '../../general/apiOptions';
import {BalanceFilter} from '../../model/forms/report/balanceFilter';

@Injectable()

export class BalanceService extends ApiOptions {
  private URL: string = environment.api.report.url.replace('{0}', this.ReportLocation());

  constructor(private http: HttpClient) {
    super();
  }

  public Report(docType: string, rptOption: string, filter: BalanceFilter): Observable<HttpEvent<Blob>> {
    const endpoint = this.URL + environment.api.report.endpoint.balance
      .replace('{0}', docType)
      .replace('{1}', rptOption);

    return this.http.request(new HttpRequest(
      'POST',
      endpoint,
      filter,
      {
        headers: this.Headers(),
        reportProgress: true,
        responseType: 'blob'
      }));
  }
}
