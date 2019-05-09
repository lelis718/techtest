using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestAPI.ViewModels;

namespace TechTestAPI.Domain.TaxProviders
{
    public class GermanyTaxProvider : BasicTaxProvider, ITaxProvider
    {
        
        public override string Country => "Germany";

        public override HomePayCalculateResponse CalculateHomePay(HomePayCalculateRequest request)
        {
            var totalGross = this.GetGrossValue(request);
            response.GrossAmount = totalGross;

            if (totalGross > 400)
            {
                response.IncomeTax = (400m * 0.25m) + ((totalGross - 400m) * 0.32m);
            }
            response.Pension = totalGross * 0.02m;

            response.NetAmount = this.GetNetValue();
            return response;
        }
    }
}
