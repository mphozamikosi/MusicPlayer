import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Genres } from 'src/app/shared/genres.model';
import { GenresService } from 'src/app/shared/genres.service';

@Component({
  selector: 'app-genres-form',
  templateUrl: './genres-form.component.html',
  styles: [
  ]
})
export class GenresFormComponent implements OnInit {

  constructor(public service: GenresService, private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  onSubmit(form:NgForm){
    if(this.service.formData.id == 0){
      this.insertGenre(form);
    }else{
      this.updateGenre(form);
    }
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData =new Genres();
  }

  insertGenre(form:NgForm){
    this.service.postGenreDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.service.openSpinner();
        this.toastr.success('Genre created successfully!', 'Genre registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Genre not created. Please try again!', 'Genre registration');
      }
    );
  }
  updateGenre(form:NgForm){
    this.service.putGenreDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Genre updated successfully!', 'Genre update');
      },
      err => {
        console.log(err);      
        this.toastr.error('Genre not updated. Please try again!', 'Genre update');
      }
    );
  }

  isLoading = false;
  
  toggleLoading = () =>{
    this.isLoading = true;
    setTimeout(()=>{
      this.isLoading = false;
    }, 3000)
  }
}
