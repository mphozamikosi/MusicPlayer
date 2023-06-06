import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule} from '@angular/forms'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import {ContactsComponent} from './contacts/contacts.component';
import {ContactsFormComponent} from './contacts/contacts-form/contacts-form.component';
import {ArtistsComponent} from './artists/artists.component';
import {ArtistsFormComponent} from './artists/artists-form/artists-form.component';
import {GenresComponent} from './genres/genres.component';
import {GenresFormComponent} from './genres/genres-form/genres-form.component';
import {AlbumsComponent} from './albums/albums.component';
import {AlbumsFormComponent} from './albums/albums-form/albums-form.component';
import {SongsComponent} from './songs/songs.component';
import {SongsFormComponent} from './songs/songs-form/songs-form.component';
import { HttpClientModule } from '@angular/common/http';
import {Ng2SearchPipeModule} from 'ng2-search-filter';
import {Ng2OrderModule} from 'ng2-order-pipe';
import {NgxPaginationModule} from 'ngx-pagination';
import {NgxCsvParserModule} from 'ngx-csv-parser';
//import {} from 'semantic-ui-css';
import {NgxSpinnerModule} from 'ngx-spinner';

@NgModule({
  declarations: [
    AppComponent,
    ContactsComponent,
    ContactsFormComponent,
    ArtistsComponent,
    ArtistsFormComponent,
    AlbumsComponent,
    AlbumsFormComponent,
    GenresComponent,
    GenresFormComponent,
    SongsComponent,
    SongsFormComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    Ng2SearchPipeModule,
    Ng2OrderModule,
    NgxPaginationModule,
    NgxCsvParserModule,
    NgxSpinnerModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
