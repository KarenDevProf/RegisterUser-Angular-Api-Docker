import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Userdetails } from "../models/Userdetails";
import { SelectOption } from "../models/SelectOption";
import { ResponseObjectModel } from "../models/ResponseObjectModel";
import { config } from '../config';

const apiUrl = `${config.apiUrl}`;
const addUserUrl = 'User/CreateUser';
const getCountriesUrl = 'getCountries';
const getProvincesUrl = 'getProvincesByCountry';


@Injectable({
  providedIn: 'root'
})
export class UserdetailsService {
  constructor(private http: HttpClient) { }

  createUser(data: Userdetails): Observable<ResponseObjectModel> {
   
    return this.http.post<ResponseObjectModel>(apiUrl+addUserUrl, data);
  }

  getCountryDropdownOptions(): Observable<SelectOption[]> {
    return this.http.get<ResponseObjectModel>(apiUrl+getCountriesUrl).pipe(
      map(response => response.data)
    );
  }

  getProvinceDropdownOptions(value: string): Observable<SelectOption[]> {
    return this.http.get<ResponseObjectModel>(`${apiUrl+getProvincesUrl}/${value}`).pipe(
      map(response => response.data)
    );
  }

}
