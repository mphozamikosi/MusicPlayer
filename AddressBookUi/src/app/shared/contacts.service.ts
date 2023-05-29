import { Injectable } from '@angular/core';
import { Music } from './contacts.model';
import {HttpClient} from '@angular/common/http'
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {

  constructor(private http:HttpClient, private spinner: NgxSpinnerService) { }

  formData:Music = new Music();
  //formDataArray:Contacts[] = [];
  readonly baseURL = 'https://localhost:44333/api/music'
  list: Music[];
  
  postContactDetails(){
    return this.http.post(this.baseURL, this.formData);
  }

  postMultipleContactDetails(formDataArray:Music[]=[]){
    return this.http.post(this.baseURL + '/PostMultipleContacts', formDataArray);
  }

  putContactDetails(){
    return this.http.put(`${this.baseURL}/${this.formData.id}`, this.formData);
  }
  
  refreshList(){
    this.http.get(this.baseURL).toPromise().then(res => this.list = res as Music[])
  }

  deleteContact(id:number){
    return this.http.delete(`${this.baseURL}/${id}`);
  }
  openSpinner(){
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
    }, 2000);
  }
}
