import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router'
import { rootRouterConfig } from './app.routes'
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// bootstrap
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { CarouselModule } from 'ngx-bootstrap/carousel';

// shared components
import { MenuSuperiorComponent } from './shared/menu-superior/menu-superior.component';
import { FooterComponent } from './shared/footer/footer.component';
import { MainPrincipalComponent } from './shared/main-principal/main-principal.component';
import { MenuLoginComponent } from './shared/menu-login/menu-login.component';
import { AcessoNegadoComponent } from './shared/acesso-negado/acesso-negado.component';

// Imports
import { MyDatePickerModule } from 'mydatepicker';
import { ToastrModule} from 'ngx-toastr';

// components
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ListaEventosComponent } from './eventos/lista-eventos/lista-eventos.component';
import { AdicionarEventoComponent } from './eventos/adicionar-evento/adicionar-evento.component';
import { InscricaoComponent } from './usuario/inscricao/inscricao.component';
import { LoginComponent } from './usuario/login/login.component';
import { MeusEventosComponent } from './eventos/meus-eventos/meus-eventos.component';

// Services
import { SeoService } from 'src/app/services/seo.service';
import { OrganizadorService } from "src/app/usuario/organizador.service";
import { AuthService } from 'src/app/shared/auth-service';
import { EventoService } from 'src/app/eventos/evento.service';
import { timeout } from 'rxjs/operators';

@NgModule({
  declarations: [/** for components*/
    AppComponent,
    MenuSuperiorComponent,
    FooterComponent,
    MainPrincipalComponent,
    HomeComponent,
    MenuLoginComponent,
    ListaEventosComponent,
    InscricaoComponent,
    LoginComponent,
    AdicionarEventoComponent,
    AcessoNegadoComponent,
    MeusEventosComponent
  ],
  imports: [/** for angular modules or others frameworks */
    BrowserModule,
    HttpModule,
    ReactiveFormsModule,
    MyDatePickerModule,
    CommonModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    CollapseModule.forRoot(),
    CarouselModule.forRoot(),
    RouterModule.forRoot(rootRouterConfig, {useHash: false})
  ],
  providers: [/** for services */
    Title,
    SeoService,
    OrganizadorService,
    AuthService,
    EventoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
