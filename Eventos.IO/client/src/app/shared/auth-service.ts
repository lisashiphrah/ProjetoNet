import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, ActivatedRoute, Router } from "@angular/router";

@Injectable()
export class AuthService implements CanActivate {

    public token: string;
    //public route;
    public user;

    constructor(private router: Router, route: ActivatedRoute) {
        //this.route = route;
        this.token = localStorage.getItem('eio.token');
        this.user = JSON.parse(localStorage.getItem('eio.user'));
    }

    canActivate(routeAc: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (!this.token) {
            this.router.navigate(['/entrar']);
            return false;
        }

        if (routeAc.data.lenght > 0) {
            let claim = routeAc.data[0]['claim'];

            if (claim) {
                let userClaims = this.user.claims.some(x => x.type === claim.nome && x.value === claim.valor);
                if (!userClaims) {
                    this.router.navigate(['/acesso-negado']);
                    return;
                }
            }
        }

        return true;
    }
}