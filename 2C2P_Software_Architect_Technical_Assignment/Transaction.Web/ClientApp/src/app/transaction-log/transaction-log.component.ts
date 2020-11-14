import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-transaction-log',
  templateUrl: './transaction-log.component.html'
})
export class TransactionLogComponent {
  private http: HttpClient;
  private baseUrl: string;

  public transactions: Transaction[];
  public currencyCodes: string[];


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.getTransactions();
  }

  private getTransactions() {
      this.http.get<Transaction[]>(this.baseUrl + 'api/transaction').subscribe(result => {
        this.transactions = result;
      }, error => console.error(error));
  }
}


interface Transaction {
  id: string;
  payment: string;
  status: string;
}

// interface WeatherForecast {
//   date: string;
//   temperatureC: number;
//   temperatureF: number;
//   summary: string;
// }
