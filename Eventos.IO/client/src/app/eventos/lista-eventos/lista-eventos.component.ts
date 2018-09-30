import { Component, OnInit } from '@angular/core';
import { SeoService, SeoModel } from "src/app/services/seo.service";
import { EventoService } from "src/app/eventos/evento.service";
import { Evento } from "src/app/eventos/evento";

@Component({
  selector: 'app-lista-eventos',
  templateUrl: './lista-eventos.component.html',
  styleUrls: ['./lista-eventos.component.css']
})
export class ListaEventosComponent implements OnInit {

public eventos: Evento[];
public errorMessage: string = "";

  constructor(seoService: SeoService,
              public eventoService: EventoService) { 

    let seoModel: SeoModel = <SeoModel>{
      title: 'Próximos Eventos',
      description: 'Lista dos próximos eventos técnicos no Brasil',
      robots: 'Index, Follow',
      keywords: 'eventos,workshops,encontros,congressos'
    };

    seoService.setSeoData(seoModel);
  }

  ngOnInit() {
    this.eventoService.obterTodos()
      .subscribe(
        eventos => this.eventos = eventos,
        error => this.errorMessage
      );
  }

}
