using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestAPI.ViewModels;

namespace TechTestAPI.Domain.TaxProviders
{
    public abstract class BasicTaxProvider:ITaxProvider
    {
        protected HomePayCalculateResponse response;
        protected BasicTaxProvider()
        {
            response = new HomePayCalculateResponse();
            response.GrossAmount = response.IncomeTax = response.NetAmount = response.Pension = 0.0m;
            response.EmployeeLocation = this.Country;
        }

        public abstract string Country { get; }
        public abstract HomePayCalculateResponse CalculateHomePay(HomePayCalculateRequest request);


        protected decimal GetGrossValue(HomePayCalculateRequest request)
        {
            return request.HourlyRate * request.HoursWorked;
        }
        protected decimal GetNetValue()
        {
            return response.GrossAmount - response.IncomeTax - response.Pension - response.UniversalSocialCharge;
        }

    }
}
