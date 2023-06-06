import { Component, OnInit } from '@angular/core';
import { Albums } from '../shared/albums.model';
import { AlbumsService } from '../shared/albums.service';
import { ToastrService } from 'ngx-toastr';
import { ViewChild } from '@angular/core';
import { NgxCsvParser, NgxCSVParserError } from 'ngx-csv-parser';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-albums',
  templateUrl: './albums.component.html',
  styles: [
  ]
})

export class AlbumsComponent implements OnInit {

  searchText: any;
  p: number = 1;
  csvRecords: Albums[] = [];
  header: boolean = true;
  data: any;
  constructor(public service: AlbumsService, private toastr:ToastrService, private ngxCsvParser: NgxCsvParser) { }
  
  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Albums){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Do you wish to delete this Album?')){
      this.service.deleteAlbum(id).subscribe(
        res=>{
          this.toastr.success('Album successfully deleted!', 'Album delete');
          this.service.refreshList();
        },err=>{
          console.log(err);
          this.toastr.error('Album not deleted! Please try again!', 'Album update');
        }
      )
    }
  }

  insertAlbums(form:any){
    this.service.postMultipleAlbumDetails(form).subscribe(
      res =>{
        //this.AlbumForm.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Albums uploaded successfully!', 'Albums registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Album not uploaded. Check that your CSV file is closed and has the appropriate headers. Please try again!', 'Albums registration');
      }
    );
  }

  Search(){
    if(this.searchText == ""){
      this.ngOnInit();
    }else{
      this.service.list = this.service.list.filter(res =>{
        return res.albumName.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase());
      });
    }
  }

  key: string = 'albumName';
  reverse: boolean =false;
  sort(key: string){
    this.key =key;
    this.reverse = !this.reverse;
  }
}
