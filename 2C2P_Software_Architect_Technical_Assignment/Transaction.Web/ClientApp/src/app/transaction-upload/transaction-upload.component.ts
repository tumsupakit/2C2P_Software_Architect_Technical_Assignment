import { Component, Inject } from '@angular/core';
import { HttpClient, HttpEventType  } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-transaction-upload',
  templateUrl: './transaction-upload.component.html'
})

export class TransactionUploadComponent {
  
  public uploadSuccess: boolean = false;
  public errorMessages: string[] = [];
  private http: HttpClient;
  private baseUrl: string;
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  public changeListener(files: FileList){
    this.uploadSuccess = false;
    this.errorMessages = [];
    if(files && files.length > 0) {
      let file : File = files.item(0);  
      const uploadedFile = new FormData();
      uploadedFile.append( 'file', file, file.name)
      const url = this.baseUrl + 'api/transaction';
      this.http.post(url, uploadedFile, { reportProgress: true, observe: 'events' }).subscribe(event => {
        if(event.type === HttpEventType.Response)
        {
          if(event.status == 200) {
            this.uploadSuccess = true;
          }
          else if(event.status == 400) {
            console.log('event error: ', event);
          }
        }
      }, (error) => {
        this.errorMessages = error.error;
      }
      );
      
    }
  }
  
}
