import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import { map, switchMap, catchError, tap } from "rxjs/operators";

import { ServiceBase } from "src/app/shared/service-base";
import { Evento, Categoria } from "src/app/eventos/evento";

@Injectable()
export class EventoService extends ServiceBase {
    constructor(private http: Http) { super(); }

    public obterUsuario() {
        return JSON.parse(localStorage.getItem('eio.user'));
    }

    obterTodos(): Observable<Evento[]> {
        let response =  this.http.get(this.UrlServiceV1 + "eventos")
            .pipe(map((res: Response) => <Evento[]>res.json()))
            catchError(super.serviceError);
        return response;
    }

    obterMeusEventos(): Observable<Evento[]> {
        let options = this.obterAuthHeader();

        let response = this.http.get(this.UrlServiceV1 + "eventos/meus-eventos", options)
            .pipe(map((res: Response) => <Evento[]>res.json()))
            catchError(super.serviceError);
        return response;
    }

    obterCategorias(): Observable<Categoria[]> {
        let response = this.http
            .get(this.UrlServiceV1 + "eventos/categorias")
            .pipe(map((res: Response) => <Categoria[]>res.json()))            
            catchError(super.serviceError);
        return response;            
    }

    registrarEvento(evento: Evento): Observable<Evento> {
        let options = this.obterAuthHeader();
        evento.id = undefined;

        let response = this.http
            .post(this.UrlServiceV1 + "eventos", evento, options)
            .pipe(map(this.extractData))
            catchError(super.serviceError);
        return response;
    };

    private extractData(response: Response) {
        let body = response.json();
        return body.data || {};
    }
}