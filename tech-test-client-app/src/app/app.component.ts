import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

	error = null;
	loading = false;
	hasResponse = false;
	countries = ['Ireland', 'Italy', 'Germany'];
	request = {
		EmployeeLocation: '',
		HourlyRate: 0.0,
		HoursWorked: 0
	};

	response: any = null;

	constructor(private http: HttpClient) {}

	public validate() {
		let ok = true;
		if ( !this.request.EmployeeLocation ) {
			ok = false;
		}
		return ok;
	}

	public calculate() {
		this.error = null;
		this.loading = true;
		this.http.post('http://tech-test.lvh.me/api/home-pay/calculate-gross-amount', this.request).subscribe(result => {
			this.hasResponse = true;
			this.response = result;
		}, (err => {
			this.error = 'API Error:' + JSON.stringify(err);
		}));

		// Mock
		// this.hasResponse = true;
		// this.response = {
		// 	EmployeeLocation: 'Ireland',
		// 	GrossAmount: 123,
		// 	NetAmount: 321,
		// 	IncomeTax: 1232,
		// 	UniversalSocialCharge: 32132,
		// 	Pension: 32123
		// };
	}

}
