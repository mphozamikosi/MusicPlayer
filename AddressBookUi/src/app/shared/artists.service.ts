import { Injectable } from '@angular/core';
import { Artists } from './artists.model';
import {HttpClient} from '@angular/common/http'
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class ArtistsService {

  constructor(private http:HttpClient, private spinner: NgxSpinnerService) { }

  formData:Artists = new Artists();
  //formDataArray:Artists[] = [];
  readonly baseURL = 'https://localhost:44333/api/artists'
  list: Artists[];
  
  postArtistDetails(){
    return this.http.post(this.baseURL, this.formData);
  }

  postMultipleArtistDetails(formDataArray:Artists[]=[]){
    return this.http.post(this.baseURL + '/PostMultipleArtists', formDataArray);
  }

  putArtistDetails(){
    return this.http.put(`${this.baseURL}/${this.formData.id}`, this.formData);
  }
  
  refreshList(){
    this.http.get(this.baseURL).toPromise().then(res => this.list = res as Artists[]);
  }

  deleteArtist(id:number){
    return this.http.delete(`${this.baseURL}/${id}`);
  }
  openSpinner(){
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
    }, 2000);
  }
}
