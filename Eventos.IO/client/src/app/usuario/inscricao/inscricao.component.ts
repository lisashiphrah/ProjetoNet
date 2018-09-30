import { Component, OnInit, AfterViewInit, OnDestroy, ViewChildren, ElementRef, ViewContainerRef } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, FormControl, FormArray, Validators, FormControlName } from '@angular/forms';

import { Router } from "@angular/router";

import { GenericValidator } from "../../utils/generic-form-validator";
import { CustomValidators, CustomFormsModule } from 'ng2-validation'

import { debounceTime } from 'rxjs/operators';
import { fromEvent, merge } from 'rxjs';
import { Observable, Subscription } from 'rxjs';

import { Organizador } from 'src/app/usuario/organizador';
import { OrganizadorService } from "src/app/usuario/organizador.service";

@Component({
  selector: 'app-inscricao',
  templateUrl: './inscricao.component.html',
  styleUrls: ['./inscricao.component.css']
})
export class InscricaoComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  public errors: any[] = [];
  inscricaoForm: FormGroup;
  organizador: Organizador;

  constructor(private fb: FormBuilder,
    private router: Router,
    private organizadorService: OrganizadorService) {

    this.validationMessages = {
      nome: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      cpf: {
        required: 'Informe o CPF',
        rangeLength: 'CPF deve conter 11 caracteres'
      },
      email: {
        required: 'Informe o e-mail',
        email: 'Email invalido'
      },
      password: {
        required: 'Informe a senha',
        minlength: 'A senha deve possuir no mínimo 6 caracteres'
      },
      confirmPassword: {
        required: 'Informe a senha novamente',
        minlength: 'A senha deve possuir no mínimo 6 caracteres',
        equalTo: 'As senhas não conferem'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
    this.organizador = new Organizador();
  }

  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  ngOnInit() {
    let password = new FormControl('', [Validators.required, Validators.minLength(6)]);
    let confirmPassword = new FormControl('', [Validators.required, Validators.minLength(6), CustomValidators.equalTo(password)]);

    this.inscricaoForm = this.fb.group({
      nome: ['', [Validators.required,
      Validators.minLength(2),
      Validators.maxLength(150)]],
      cpf: ['', [Validators.required,
      CustomValidators.rangeLength([11, 11])]],
      email: ['', [Validators.required,
      CustomValidators.email]],
      password: password,
      confirmPassword: confirmPassword
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(this.inscricaoForm.valueChanges, ...controlBlurs).pipe(debounceTime(100)).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.inscricaoForm);
    });
  }

  adicionarOrganizador() {
    if (this.inscricaoForm.dirty && this.inscricaoForm.valid) {
      let p = Object.assign({}, this.organizador, this.inscricaoForm.value);  
      
      this.organizadorService.registrarOrganizador(p)
          .subscribe(
          result => { this.onSaveComplete(result) },
          error => {
            this.errors = JSON.parse(error._body).errors;
          });
    }
  }

  onSaveComplete(response: any): void {
    this.inscricaoForm.reset();
    this.errors = [];

    localStorage.setItem('eio.token', response.result.access_token);
    localStorage.setItem('eio.user', JSON.stringify(response.result.user));

    this.router.navigate(['/home']);
  }

  ngOnDestroy(): void {
    //throw new Error('Method not implemented.');
  }
}
