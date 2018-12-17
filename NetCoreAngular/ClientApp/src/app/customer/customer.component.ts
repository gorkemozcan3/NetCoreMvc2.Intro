import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-customer-component',
  templateUrl: './customer.component.html'
})
export class CustomerComponent {
  public customers: Customer[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Customer[]>(baseUrl + 'api/Customers/GetCustomers').subscribe(result => {
      this.customers = result;
    }, error => console.error(error));
  }
}

interface Customer {
  id: number;
  firstName: string;
  lastName: string;
}
