import { HttpRequest, HttpInterceptor, HttpHandler } from "@angular/common/http";
import { AuthenticationService } from '../_services/authentication.service';
import { Injectable } from "@angular/core";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authenicationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler) {
        let currentUser = this.authenicationService.currentUserValue;
        if (currentUser && currentUser.token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.token}`
                }
            });
        }

        return next.handle(request);
    }
}