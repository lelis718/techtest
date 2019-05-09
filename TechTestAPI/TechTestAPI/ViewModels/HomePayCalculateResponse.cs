using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTestAPI.ViewModels
{
    public class HomePayCalculateResponse
    {
        public String EmployeeLocation { get; set; }
        public decimal GrossAmount { get; set; }

        public decimal IncomeTax { get; set; }
        public decimal UniversalSocialCharge { get; set; }
        public decimal Pension { get; set; }
        public decimal NetAmount { get; set; }

    }
}
