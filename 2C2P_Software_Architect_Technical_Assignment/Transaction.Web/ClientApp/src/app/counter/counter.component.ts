import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Observable } from 'rxjs';

const MAX_SIZE: number = 1048576;

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})

export class CounterComponent {
  
  public currentCount = 0;
  private http: HttpClient;
  private baseUrl: string;

  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }


  public incrementCounter() {
    this.currentCount++;
  }

  public changeListener(files: FileList){
    if(files && files.length > 0) {
       let file : File = files.item(0); 

       if(file.size > MAX_SIZE) {
         alert('The file is too large');
       }

       const uploadedFile = new FormData();
       uploadedFile.append( 'file', file, file.name)
       const url = this.baseUrl + 'api/transaction';
       return this.http.post(url, uploadedFile);
      }
  }
  
}
