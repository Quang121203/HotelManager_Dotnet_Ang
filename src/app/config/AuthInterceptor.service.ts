import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { Router } from '@angular/router';
import { SnackbarService } from '../services/snackbar.service';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    private isRefreshing = false;

    constructor(private router: Router, private snackbarService: SnackbarService,
        private cookieService: CookieService, private authService: AuthService,
    ) {
    }

    // private tokenExpired(token: string) {
    //     const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
    //     return (Math.floor((new Date).getTime() / 1000)) >= expiry;
    // }

    // private async refresh() {
    //     var accessToken = this.cookieService.get('accessToken');

    //     if (this.tokenExpired(accessToken) && !this.isRefreshing) {
    //         this.isRefreshing = true;
    //         var refeshToken = this.cookieService.get('refeshToken');

    //         if (refeshToken) {
    //             var response = await this.authService.refeshToken(refeshToken).toPromise();
    //             if (response && response.ec == 0) {
    //                 this.cookieService.set('accessToken', response.dt.accessToken);
    //                 this.cookieService.set('refeshToken', response.dt.refeshToken);
    //                 this.isRefreshing = false;
    //             }
    //             else {
    //                 this.router.navigate(['/login']);
    //                 this.snackbarService.openSnackBar("Please login");
    //             }

    //         };
    //     }
    // }


    private handleAuthError(err: HttpErrorResponse): Observable<any> {
        if (err.status === 401) {
            this.router.navigate(['/login']);
            this.snackbarService.openSnackBar("Please login");
            return of(err.message);
        }
        if (err.status === 403) {
            this.router.navigate(['/hotel/profile']);
            this.snackbarService.openSnackBar("You dont have permission to");
            return of(err.message);
        }
        return of(err.message);
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (request.url.endsWith('/Auth/Login')) {
            return next.handle(request);
        }

        var accessToken = this.cookieService.get('accessToken');
       
        const modifiedHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            Authorization: `Bearer ${accessToken}`
        });

        // Sao chép request với header mới
        const modifiedRequest = request.clone({ headers: modifiedHeaders });


        return next.handle(modifiedRequest).pipe(catchError(x => this.handleAuthError(x)));
    }
}