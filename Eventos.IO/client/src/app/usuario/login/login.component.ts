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
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  public errors: any[] = [];
  loginForm: FormGroup;
  organizador: Organizador;

  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  constructor(private fb: FormBuilder,
              private organizadorService: OrganizadorService,
              private router: Router) { 

    this.validationMessages = {
      email: {
        required: 'Informe o e-mail',
        email: 'Email invalido'
      },
      password: {
        required: 'Informe a senha',
        minlength: 'A senha deve possuir no mínimo 6 caracteres'
      },
    };
    this.genericValidator = new GenericValidator(this.validationMessages);
    this.organizador = new Organizador();
  }

  ngOnInit() {
    this.loginForm = this.fb.group({

      email: ['', [Validators.required, CustomValidators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(this.loginForm.valueChanges, ...controlBlurs).pipe(debounceTime(100)).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.loginForm);
    });
  }

  login() {
    if (this.loginForm.dirty && this.loginForm.valid) {
      let p = Object.assign({}, this.organizador, this.loginForm.value);

      this.organizadorService.login(p)
        .subscribe(
          result => { this.onSaveComplete(result) },
          error => {
            this.errors = JSON.parse(error._body).errors;
        });
    }
  }

  onSaveComplete(response: any): void {
    this.loginForm.reset();
    this.errors = [];

    localStorage.setItem('eio.token', response.result.access_token);
    localStorage.setItem('eio.user', JSON.stringify(response.result.user));

    this.router.navigate(['/proximos-eventos']);
  }

}
