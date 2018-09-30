import { Component, OnInit } from '@angular/core';
import { EventoService } from "src/app/eventos/evento.service";
import { Evento } from "src/app/eventos/evento";

@Component({
  selector: 'app-meus-eventos',
  templateUrl: './meus-eventos.component.html',
  styleUrls: ['./meus-eventos.component.css']
})
export class MeusEventosComponent implements OnInit {
public eventos: Evento[];
public errorMessage: string = "";

  constructor(public eventoService: EventoService) { 
  }

  ngOnInit() {
    this.eventoService.obterMeusEventos()
      .subscribe(
        eventos => this.eventos = eventos,
        error => this.errorMessage
      );
  }
}
