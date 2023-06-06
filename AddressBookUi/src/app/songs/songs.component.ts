import { Component, OnInit } from '@angular/core';
import { Songs } from '../shared/songs.model';
import { SongsService } from '../shared/songs.service';
import { ToastrService } from 'ngx-toastr';
import { ViewChild } from '@angular/core';
import { NgxCsvParser, NgxCSVParserError } from 'ngx-csv-parser';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-songs',
  templateUrl: './songs.component.html',
  styles: [
  ]
})

export class SongsComponent implements OnInit {

  searchText: any;
  p: number = 1;
  csvRecords: Songs[] = [];
  header: boolean = true;
  data: any;
  constructor(public service: SongsService, private toastr:ToastrService, private ngxCsvParser: NgxCsvParser) { }
  
  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Songs){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Do you wish to delete this Song?')){
      this.service.deleteSong(id).subscribe(
        res=>{
          this.toastr.success('Song successfully deleted!', 'Song delete');
          this.service.refreshList();
        },err=>{
          console.log(err);
          this.toastr.error('Song not deleted! Please try again!', 'Song update');
        }
      )
    }
  }

  insertSongs(form:any){
    this.service.postMultipleSongDetails(form).subscribe(
      res =>{
        //this.SongForm.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Songs uploaded successfully!', 'Songs registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Song not uploaded. Check that your CSV file is closed and has the appropriate headers. Please try again!', 'Songs registration');
      }
    );
  }

  Search(){
    if(this.searchText == ""){
      this.ngOnInit();
    }else{
      this.service.list = this.service.list.filter(res =>{
        return res.songName.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase());
      });
    }
  }

  key: string = 'songName';
  reverse: boolean =false;
  sort(key: string){
    this.key =key;
    this.reverse = !this.reverse;
  }
}
