using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechTestAPI.ViewModels;

namespace TechTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePayController : ControllerBase
    {

        public Task<IActionResult> CalculateGrossAmount(HomePayCalculateRequest employeeRequest)
        {
            throw new NotImplementedException();
        }
    }
}