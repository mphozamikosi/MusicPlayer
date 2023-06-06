import { Injectable } from '@angular/core';
import { Genres } from './genres.model';
import {HttpClient} from '@angular/common/http'
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class GenresService {

  constructor(private http:HttpClient, private spinner: NgxSpinnerService) { }

  formData:Genres = new Genres();
  //formDataArray:Genres[] = [];
  readonly baseURL = 'https://localhost:44333/api/genres'
  list: Genres[];
  
  postGenreDetails(){
    return this.http.post(this.baseURL, this.formData);
  }

  postMultipleGenreDetails(formDataArray:Genres[]=[]){
    return this.http.post(this.baseURL + '/PostMultipleGenres', formDataArray);
  }

  putGenreDetails(){
    return this.http.put(`${this.baseURL}/${this.formData.id}`, this.formData);
  }
  
  refreshList(){
    this.http.get(this.baseURL).toPromise().then(res => this.list = res as Genres[])
  }

  deleteGenre(id:number){
    return this.http.delete(`${this.baseURL}/${id}`);
  }
  openSpinner(){
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
    }, 2000);
  }
}
