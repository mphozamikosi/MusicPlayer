import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Albums } from 'src/app/shared/albums.model';
import { AlbumsService } from 'src/app/shared/albums.service';

@Component({
  selector: 'app-albums-form',
  templateUrl: './albums-form.component.html',
  styles: [
  ]
})
export class AlbumsFormComponent implements OnInit {

  constructor(public service: AlbumsService, private toastr:ToastrService) { }
  p: number = 1;
  ngOnInit(): void {
  }
  onSubmit(form:NgForm){
    if(this.service.formData.id == 0){
      this.insertAlbum(form);
    }else{
      this.updateAlbum(form);
    }
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData =new Albums();
  }

  insertAlbum(form:NgForm){
    this.service.postAlbumDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.service.openSpinner();
        this.toastr.success('Album created successfully!', 'Album registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Album not created. Please try again!', 'Album registration');
      }
    );
  }
  updateAlbum(form:NgForm){
    this.service.putAlbumDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Album updated successfully!', 'Album update');
      },
      err => {
        console.log(err);      
        this.toastr.error('Album not updated. Please try again!', 'Album update');
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

  key: string = 'albumName';
  reverse: boolean =false;
  sort(key: string){
    this.key =key;
    this.reverse = !this.reverse;
  }
}
