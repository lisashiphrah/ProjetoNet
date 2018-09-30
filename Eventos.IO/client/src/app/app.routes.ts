import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ListaEventosComponent } from './eventos/lista-eventos/lista-eventos.component';
import { Route } from '@angular/compiler/src/core';
import { InscricaoComponent } from './usuario/inscricao/inscricao.component';
import { LoginComponent } from './usuario/login/login.component';
import { AdicionarEventoComponent } from './eventos/adicionar-evento/adicionar-evento.component';

import { AuthService } from "src/app/shared/auth-service";
import { AcessoNegadoComponent } from './shared/acesso-negado/acesso-negado.component';
import { MeusEventosComponent } from './eventos/meus-eventos/meus-eventos.component';

export const rootRouterConfig: Routes = [
    {path: '', redirectTo: 'home', pathMatch: 'full' },
    {path: 'home', component: HomeComponent },
    {path: 'proximos-eventos', component: ListaEventosComponent },
    {path: 'inscricao', component: InscricaoComponent},
    {path: 'entrar', component: LoginComponent},
    {path: 'novo-evento', canActivate: [AuthService], component: AdicionarEventoComponent, data: [{ claim: { nome: 'Eventos', valor: 'Gravar'}}]},
    {path: 'meus-eventos', canActivate: [AuthService], component: MeusEventosComponent},
    {path: 'acesso-negado', component: AcessoNegadoComponent},
]