import { Component, OnInit } from '@angular/core';
import { Genres } from '../shared/genres.model';
import { GenresService } from '../shared/genres.service';
import { ToastrService } from 'ngx-toastr';
import { ViewChild } from '@angular/core';
import { NgxCsvParser, NgxCSVParserError } from 'ngx-csv-parser';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-genres',
  templateUrl: './genres.component.html',
  styles: [
  ]
})

export class GenresComponent implements OnInit {

  searchText: any;
  p: number = 1;
  csvRecords: Genres[] = [];
  header: boolean = true;
  data: any;
  constructor(public service: GenresService, private toastr:ToastrService, private ngxCsvParser: NgxCsvParser) { }
  
  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Genres){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Do you wish to delete this Genre?')){
      this.service.deleteGenre(id).subscribe(
        res=>{
          this.toastr.success('Genre successfully deleted!', 'Genre delete');
          this.service.refreshList();
        },err=>{
          console.log(err);
          this.toastr.error('Genre not deleted! Please try again!', 'Genre update');
        }
      )
    }
  }

  insertGenres(form:any){
    this.service.postMultipleGenreDetails(form).subscribe(
      res =>{
        //this.GenreForm.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Genres uploaded successfully!', 'Genres registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Genre not uploaded. Check that your CSV file is closed and has the appropriate headers. Please try again!', 'Genres registration');
      }
    );
  }

  Search(){
    if(this.searchText == ""){
      this.ngOnInit();
    }else{
      this.service.list = this.service.list.filter(res =>{
        return res.genreName.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase());
      });
    }
  }

  key: string = 'genreName';
  reverse: boolean =false;
  sort(key: string){
    this.key =key;
    this.reverse = !this.reverse;
  }
}
