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
  public currencyCodes: string[] = [];
  public currencyCodeFilter: string = "";
  public dateFromFilter: Date;
  public dateToFilter: Date;
  public statusFilter: number = undefined;
  

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.getTransactions();
    this.getCurrency();
  }

  public btnSearchClick() {
    this.getTransactions();
  }

  private getTransactions() {
    let filter: string = "?currencyCode=" + this.currencyCodeFilter 
    + "&dateFrom=" + this.dateFromFilter 
    + "&dateTo=" + this.dateToFilter 
    + "&status=" + this.statusFilter;
    
    this.http.get<Transaction[]>(this.baseUrl + 'api/transaction' + filter).subscribe(result => {
      this.transactions = result;
    }, error => console.error(error));
  }

  private getCurrency() {
    this.http.get<string[]>(this.baseUrl + 'api/currency').subscribe(result => {
      this.currencyCodes = result;
    }, error => console.error(error));
  }
}

interface Transaction {
  id: string;
  currencyCode: string;
  payment: string;
  status: string;
}
