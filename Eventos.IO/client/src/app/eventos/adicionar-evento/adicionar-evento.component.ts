import { Component, OnInit, AfterViewInit, ViewChildren, ElementRef, ViewContainerRef } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, FormControl, FormArray, Validators, FormControlName } from '@angular/forms';

import { Router } from "@angular/router";

import { IMyOptions, IMyDateModel } from 'mydatepicker';
import { Evento, Endereco, Categoria } from "../evento";
import { GenericValidator } from "../../utils/generic-form-validator";
import { DateUtils } from "../../utils/date-utils";
import { ToastrService } from 'ngx-toastr';

import { debounceTime } from 'rxjs/operators';
import { fromEvent, merge } from 'rxjs';
import { Observable, Subscription } from 'rxjs';

import { EventoService } from '../evento.service';

@Component({
  selector: 'app-adicionar-evento',
  templateUrl: './adicionar-evento.component.html',
  styleUrls: ['./adicionar-evento.component.css']
})
export class AdicionarEventoComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  public myDatePickerOptions = DateUtils.getMyDatePickerOptions();
  
  public errors: any[] = [];
  public eventoForm: FormGroup;
  public evento: Evento;
  public categorias: Categoria[];
  public gratuito: boolean;
  public online: boolean;

  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  constructor(private fb: FormBuilder,
              private eventoService: EventoService,
              private toastr: ToastrService,
              private router: Router) {

    this.validationMessages = {
      nome: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      dataInicio: {
        required: 'Informe a data de início'
      },
      dataFim: {
        required: 'Informe a data de encerramento'
      },
      categoriaId: {
        required: 'Informe a categoria'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
    this.evento = new Evento();
    this.evento.endereco = new Endereco();
  }

  ngOnInit() {
    this.eventoForm = this.fb.group({
      nome: ['', [Validators.required,
      Validators.minLength(2),
      Validators.maxLength(150)]],
      categoriaId: ['', Validators.required],
      descricaoCurta: '',
      descricaoLonga: '',
      dataInicio: ['', Validators.required],
      dataFim: ['', Validators.required],
      gratuito: '',
      valor: '0',
      online: '',
      nomeEmpresa: '',
      logradouro: '',
      numero: '',
      complemento: '',
      bairro: '',
      cep: '',
      cidade: '',
      estado: '',
    });

    this.eventoService.obterCategorias()
    .subscribe(
      categorias => this.categorias = categorias,
      error => this.errors
    );
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(this.eventoForm.valueChanges, ...controlBlurs).pipe(debounceTime(100)).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.eventoForm);
    });
  }

  adicionarEvento(){
    if (this.eventoForm.dirty && this.eventoForm.valid) {
      let user = this.eventoService.obterUsuario();

      let p = Object.assign({}, this.evento, this.eventoForm.value)

      p.organizadorId = user.id;
      p.dataInicio = DateUtils.getMyDatePickerDate(p.dataInicio);
      p.dataFim = DateUtils.getMyDatePickerDate(p.dataFim);
      p.endereco.logradouro = p.logradouro;
      p.endereco.numero = p.numero;
      p.endereco.complemento = p.complemento;
      p.endereco.bairro = p.bairro;
      p.endereco.cep = p.cep;
      p.endereco.cidade = p.cidade;
      p.endereco.estado = p.estado;

      this.eventoService.registrarEvento(p)
        .subscribe(
        result => { this.onSaveComplete() },
        error => { this.onError(error) });
    }
  }

  onError(error): void {
    this.toastr.error('Ocorreu um erro no processamento', 'Ops! :(');    
    this.errors = JSON.parse(error._body).errors;
    console.log(error);
  }

  onSaveComplete(): void {
    this.eventoForm.reset();
    this.errors = [];

    this.toastr.success('Evento Registrado com Sucesso!', 'Oba :D')
    setTimeout(() => {      
      this.router.navigateByUrl('/proximos-eventos');
      this.toastr.clear();
    }, 2500);
  }

}
