import { Http, Headers, Response, RequestOptions } from '@angular/http';

import { debounceTime } from 'rxjs/operators';
import { fromEvent, merge } from 'rxjs';
import { Observable, Subscription } from 'rxjs';

import { Organizador } from "../usuario/organizador";

export abstract class ServiceBase {
    public Token: string = "";

    constructor() {
        this.Token = localStorage.getItem('eio.token');
    }

    protected UrlServiceV1: string = "http://localhost:8287/api/v1/";

    protected obterAuthHeader(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append('Authorization', `Bearer ${this.Token}`);
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    protected serviceError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(error);
        return Observable.throw(error);
    }
}