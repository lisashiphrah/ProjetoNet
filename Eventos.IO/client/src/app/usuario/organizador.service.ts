import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable, Subscription, throwError} from 'rxjs';

import { Organizador } from "src/app/usuario/organizador";
import { map, switchMap, catchError, tap } from "rxjs/operators";

@Injectable()
export class OrganizadorService {
    constructor(private http: Http) {

    }

    registrarOrganizador(organizador: Organizador): Observable<Organizador> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        //let jsons = JSON.stringify(organizador);
        let response = this.http
            .post("http://localhost:8287/api/v1/nova-conta", organizador, options)
            .pipe(map(this.extractData))
            catchError(this.serviceError);

        return response;
    }

    login(organizador: Organizador): Observable<Organizador> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        let response = this.http
            .post("http://localhost:8287/api/v1/conta", organizador, options)
            .pipe(map(this.extractData))
            catchError(this.serviceError);

        return response;
    }

    private extractData(response: Response) {
        let body = response.json();
        return body.data || {};
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