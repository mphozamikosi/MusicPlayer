import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Songs } from 'src/app/shared/songs.model';
import { SongsService } from 'src/app/shared/songs.service';

@Component({
  selector: 'app-songs-form',
  templateUrl: './songs-form.component.html',
  styles: [
  ]
})
export class SongsFormComponent implements OnInit {

  constructor(public service: SongsService, private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  onSubmit(form:NgForm){
    if(this.service.formData.id == 0){
      this.insertSong(form);
    }else{
      this.updateSong(form);
    }
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData =new Songs();
  }

  insertSong(form:NgForm){
    this.service.postSongDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.service.openSpinner();
        this.toastr.success('Song created successfully!', 'Song registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Song not created. Please try again!', 'Song registration');
      }
    );
  }
  updateSong(form:NgForm){
    this.service.putSongDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Song updated successfully!', 'Song update');
      },
      err => {
        console.log(err);      
        this.toastr.error('Song not updated. Please try again!', 'Song update');
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
