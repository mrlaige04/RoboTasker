import {inject, Injectable, signal} from '@angular/core';
import {BaseHttp} from '../base/base-http';
import {LoginRequest} from '../../models/auth/login-request';
import {Observable} from 'rxjs';
import {AccessToken} from '../../models/auth/access-token';
import {ForgotPasswordRequest} from '../../models/auth/forgot-password-request';
import {Success} from '../../models/success';
import {ResetPasswordRequest} from '../../models/auth/reset-password-request';
import {ChangePasswordRequest} from '../../models/auth/change-password-request';
import {CurrentUserService} from '../user/current-user.service';
import {LoginResponse} from '../../models/auth/login-response';
import {RegisterRequest} from '../../models/auth/register-request';
import {jwtDecode} from 'jwt-decode'
import {CreateCompanyRequest} from '../../models/auth/create-company-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserService = inject(CurrentUserService);
  private base = inject(BaseHttp);
  private readonly baseUrl = 'auth'
  private readonly storageKey = 'JWT';

  private _accessToken = signal<AccessToken | null>(this.getSavedToken());
  public accessToken = this._accessToken.asReadonly();

  private _isAuthenticated = signal(this.getSavedToken() !== null);
  public isAuthenticated = this._isAuthenticated.asReadonly();

  private _isSuperAdmin = signal<boolean>(this.getSavedIsSuperAdmin());
  public isSuperAdmin = this._isSuperAdmin.asReadonly();

  createCompany(data: CreateCompanyRequest): Observable<Success> {
    const url = `${this.baseUrl}/new-company`;
    return this.base.post<CreateCompanyRequest, Success>(url, data);
  }

  register(data: RegisterRequest): Observable<Success> {
    const url = `${this.baseUrl}/register`;
    return this.base.post<RegisterRequest, Success>(url, data);
  }

  login(data: LoginRequest): Observable<LoginResponse> {
    const url = `${this.baseUrl}/login`;
    return this.base.post<LoginRequest, LoginResponse>(url, data);
  }

  forgotPassword(data: ForgotPasswordRequest): Observable<Success> {
    const url = `${this.baseUrl}/forgot-password`;
    return this.base.post<ForgotPasswordRequest, Success>(url, data);
  }

  resetPassword(data: ResetPasswordRequest): Observable<Success> {
    const url = `${this.baseUrl}/reset-password`;
    return this.base.post<ResetPasswordRequest, Success>(url, data);
  }

  changePassword(data: ChangePasswordRequest): Observable<Success> {
    const url = `${this.baseUrl}/change-password`;
    return this.base.post<ChangePasswordRequest, Success>(url, data);
  }

  refreshToken(): Observable<AccessToken> {
    const token = this.accessToken();
    const data = {
      token: token?.accessToken,
      refreshToken: token?.refreshToken
    };
    const url = `${this.baseUrl}/refresh-token`;
    return this.base.post<any, AccessToken>(url, data);
  }

  handleSuccessLogin(result: LoginResponse) {
    localStorage.setItem(this.storageKey, JSON.stringify(result.token));

    this._accessToken.set(result.token);
    this._isAuthenticated.set(true);

    this.currentUserService.setCurrentUser(result.user);

    const decodedToken = jwtDecode<any>(result.token.accessToken);
    const isSuperAdmin = decodedToken['IsSuperAdmin'] === 'true';
    this._isSuperAdmin.set(isSuperAdmin);
  }

  updateToken(token: AccessToken) {
    localStorage.setItem(this.storageKey, JSON.stringify(token));
    this._accessToken.set(token);
    this._isAuthenticated.set(true);

    const decodedToken = jwtDecode<any>(token.accessToken);
    const isSuperAdmin = decodedToken['IsSuperAdmin'] === 'true';
    this._isSuperAdmin.set(isSuperAdmin);
  }

  private getSavedToken() {
    const json = localStorage.getItem(this.storageKey);
    if (json) {
      const token = JSON.parse(json) as AccessToken;
      if (token) {
        return token;
      }
    }

    return null;
  }

  private getSavedIsSuperAdmin() {
    const json = localStorage.getItem(this.storageKey);
    if (json) {
      const token = JSON.parse(json) as AccessToken;
      if (token) {
        const decodedToken = jwtDecode<any>(token.accessToken);
        return decodedToken['IsSuperAdmin'] === 'true';
      }
    }

    return false;
  }

  logout() {
    localStorage.removeItem(this.storageKey);

    this._accessToken.set(null);
    this._isAuthenticated.set(false);

    this.currentUserService.clearCurrentUser();

    location.reload();
  }
}
