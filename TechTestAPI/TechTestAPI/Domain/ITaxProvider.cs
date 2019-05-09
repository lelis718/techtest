using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestAPI.ViewModels;

namespace TechTestAPI.Domain
{
    interface ITaxProvider
    {
        String Country { get; }
        HomePayCalculateResponse CalculateHomePay(HomePayCalculateRequest request);
    }
}
