using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestAPI.ViewModels;

namespace TechTestAPI.Domain.TaxProviders
{
    public class IrelandTaxProvider : BasicTaxProvider, ITaxProvider
    {

        public override string Country => "Ireland";

        public override HomePayCalculateResponse CalculateHomePay(HomePayCalculateRequest request)
        {
            var totalGross = this.GetGrossValue(request);
            response.GrossAmount = totalGross;
            if (totalGross > 600)
            {
                response.IncomeTax = (600m * 0.25m) + ((totalGross - 600m) * 0.4m);
            }
            if (totalGross > 500)
            {
                response.UniversalSocialCharge = (500m * 0.07m) + ((totalGross - 500m) * 0.08m);
            }
            response.Pension = totalGross * 0.04m;
            response.NetAmount = this.GetNetValue();
            return response;
        }
    }
}
