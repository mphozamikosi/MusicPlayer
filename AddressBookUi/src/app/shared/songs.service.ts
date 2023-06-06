import { Injectable } from '@angular/core';
import { Songs } from './songs.model';
import {HttpClient} from '@angular/common/http'
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class SongsService {

  constructor(private http:HttpClient, private spinner: NgxSpinnerService) { }

  formData:Songs = new Songs();
  //formDataArray:Songs[] = [];
  readonly baseURL = 'https://localhost:44333/api/songs'
  list: Songs[];
  
  postSongDetails(){
    return this.http.post(this.baseURL, this.formData);
  }

  postMultipleSongDetails(formDataArray:Songs[]=[]){
    return this.http.post(this.baseURL + '/PostMultipleSongs', formDataArray);
  }

  putSongDetails(){
    return this.http.put(`${this.baseURL}/${this.formData.id}`, this.formData);
  }
  
  refreshList(){
    this.http.get(this.baseURL).toPromise().then(res => this.list = res as Songs[])
  }

  deleteSong(id:number){
    return this.http.delete(`${this.baseURL}/${id}`);
  }
  openSpinner(){
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
    }, 2000);
  }
}
