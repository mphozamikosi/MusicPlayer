import { Injectable } from '@angular/core';
import { Albums } from './albums.model';
import {HttpClient} from '@angular/common/http'
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class AlbumsService {

  constructor(private http:HttpClient, private spinner: NgxSpinnerService) { }

  formData:Albums = new Albums();
  //formDataArray:Albums[] = [];
  readonly baseURL = 'https://localhost:44333/api/albums'
  list: Albums[];
  
  postAlbumDetails(){
    return this.http.post(this.baseURL, this.formData);
  }

  postMultipleAlbumDetails(formDataArray:Albums[]=[]){
    return this.http.post(this.baseURL + '/PostMultipleAlbums', formDataArray);
  }

  putAlbumDetails(){
    return this.http.put(`${this.baseURL}/${this.formData.id}`, this.formData);
  }
  
  refreshList(){
    this.http.get(this.baseURL).toPromise().then(res => this.list = res as Albums[])
  }

  deleteAlbum(id:number){
    return this.http.delete(`${this.baseURL}/${id}`);
  }
  openSpinner(){
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
    }, 2000);
  }
}
