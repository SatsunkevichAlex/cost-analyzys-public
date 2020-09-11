import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

import { environment } from "../../environments/environment.dev";
import { User } from "../_models/user";

@Injectable()
export class UserService {
    constructor(private http: HttpClient) { }

    public getAll() {
        return this.http.get<User[]>(`${environment}/users`);
    }
}