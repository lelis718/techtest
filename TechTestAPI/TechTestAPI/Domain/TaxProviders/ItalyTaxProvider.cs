using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestAPI.ViewModels;

namespace TechTestAPI.Domain.TaxProviders
{
    public class ItalyTaxProvider : BasicTaxProvider, ITaxProvider
    {

        public override string Country => "Italy";

        public override HomePayCalculateResponse CalculateHomePay(HomePayCalculateRequest request)
        {
            var totalGross = this.GetGrossValue(request);
            response.GrossAmount = totalGross;
            
            response.IncomeTax = totalGross * 0.25m;
            if (totalGross > 500)
            {
                int INPSRate = (int)((totalGross - 500m) / 100.0m);
                var inpstax = (500m * 0.09m) + ((totalGross - 500m) * (0.001m * INPSRate));
                response.IncomeTax += inpstax;
            }

            response.NetAmount = this.GetNetValue();
            return response;
        }
    }
}
