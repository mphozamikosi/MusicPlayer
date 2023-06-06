import { Component, OnInit } from '@angular/core';
import { Artists } from '../shared/artists.model';
import { ArtistsService } from '../shared/artists.service';
import { ToastrService } from 'ngx-toastr';
import { ViewChild } from '@angular/core';
import { NgxCsvParser, NgxCSVParserError } from 'ngx-csv-parser';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-artists',
  templateUrl: './artists.component.html',
  styles: [
  ]
})

export class ArtistsComponent implements OnInit {

  searchText: any;
  p: number = 1;
  csvRecords: Artists[] = [];
  header: boolean = true;
  data: any;
  constructor(public service: ArtistsService, private toastr:ToastrService, private ngxCsvParser: NgxCsvParser) { }
  
  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Artists){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Do you wish to delete this Artist?')){
      this.service.deleteArtist(id).subscribe(
        res=>{
          this.toastr.success('Artist successfully deleted!', 'Artist delete');
          this.service.refreshList();
        },err=>{
          console.log(err);
          this.toastr.error('Artist not deleted! Please try again!', 'Artist update');
        }
      )
    }
  }

  insertArtists(form:any){
    this.service.postMultipleArtistDetails(form).subscribe(
      res =>{
        //this.ArtistForm.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Artists uploaded successfully!', 'Artists registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Artist not uploaded. Check that your CSV file is closed and has the appropriate headers. Please try again!', 'Artists registration');
      }
    );
  }

  Search(){
    if(this.searchText == ""){
      this.ngOnInit();
    }else{
      this.service.list = this.service.list.filter(res =>{
        return res.artistName.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase());
      });
    }
  }

  key: string = 'artistName';
  reverse: boolean =false;
  sort(key: string){
    this.key =key;
    this.reverse = !this.reverse;
  }
}
