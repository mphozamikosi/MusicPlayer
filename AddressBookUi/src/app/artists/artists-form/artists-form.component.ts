import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Artists } from 'src/app/shared/artists.model';
import { ArtistsService } from 'src/app/shared/artists.service';

@Component({
  selector: 'app-artists-form',
  templateUrl: './artists-form.component.html',
  styles: [
  ]
})
export class ArtistsFormComponent implements OnInit {

  constructor(public service: ArtistsService, private toastr:ToastrService) { }
  p: number = 1;
  ngOnInit(): void {
  }
  onSubmit(form:NgForm){
    if(this.service.formData.id == 0){
      this.insertArtist(form);
    }else{
      this.updateArtist(form);
    }
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData =new Artists();
  }

  insertArtist(form:NgForm){
    this.service.postArtistDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.service.openSpinner();
        this.toastr.success('Artist created successfully!', 'Artist registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Artist not created. Please try again!', 'Artist registration');
      }
    );
  }
  updateArtist(form:NgForm){
    this.service.putArtistDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Artist updated successfully!', 'Artist update');
      },
      err => {
        console.log(err);      
        this.toastr.error('Artist not updated. Please try again!', 'Artist update');
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

  key: string = 'artistName';
  reverse: boolean =false;
  sort(key: string){
    this.key =key;
    this.reverse = !this.reverse;
  }
}
