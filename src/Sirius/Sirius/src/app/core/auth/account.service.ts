import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {AuthService} from './auth.service';
import {Observable} from 'rxjs/Observable';
import {_throw} from 'rxjs/observable/throw';
import {catchError, map} from 'rxjs/operators';

@Injectable()
export class AccountService {
  constructor(
    private http: HttpClient,
    private authService: AuthService) {
  }

  /**
   * Registers a new user.
   * @param model User's data
   * @return An IdentityResult
   */
  public register(model: any): Observable<any> {
    const body: string = JSON.stringify(model);

    // Sends an authenticated request.
    return this.http.post('/api/account', body, {
      headers: new HttpHeaders().set('Content-Type', 'application/json')
    }).pipe(
      map((response: Response) => {
        return response;
      }),
      catchError((error: any) => {
        return _throw(error);
      }));
  }
}
