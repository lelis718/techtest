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

        [AcceptVerbs("Post")]
        public async Task<IActionResult> CalculateGrossAmount(HomePayCalculateRequest employeeRequest)
        {
            validateRequest(employeeRequest);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var country = employeeRequest.EmployeeLocation;
            var response = new HomePayCalculateResponse();
            response.GrossAmount = response.IncomeTax = response.NetAmount = response.Pension = 0.0m;

            response.EmployeeLocation = country;
            switch (country.ToLower())
            {
                case "ireland":
                    CalculateTaxForIreland(employeeRequest, response);
                    break;
                case "italy":
                    CalculateTaxForItaly(employeeRequest, response);
                    break;
                case "germany":
                    CalculateTaxForGermany(employeeRequest, response);
                    break;
            }
            return Ok(response);
        }

        private void validateRequest(HomePayCalculateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EmployeeLocation))
            {
                ModelState.AddModelError("EmployeeLocation", "Location is mandatory");
            }
        }

        private decimal GetGross(HomePayCalculateRequest employeeRequest)
        {
            return employeeRequest.HourlyRate * employeeRequest.HoursWorked;
        }
        private decimal GetNetValue(HomePayCalculateResponse employeeResponse)
        {
            return employeeResponse.GrossAmount - employeeResponse.IncomeTax - employeeResponse.Pension - employeeResponse.UniversalSocialCharge;
        }

        private void CalculateTaxForIreland(HomePayCalculateRequest request, HomePayCalculateResponse response)
        {
            var totalGross = GetGross(request);
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
            response.NetAmount = GetNetValue(response);
        }

        private void CalculateTaxForItaly(HomePayCalculateRequest request, HomePayCalculateResponse response)
        {
            var totalGross = GetGross(request);
            response.GrossAmount = totalGross;
            response.IncomeTax = totalGross * 0.25m;
            if (totalGross > 500)
            {
                int INPSRate = (int)((totalGross - 500m) / 100.0m);
                var inpstax = (500m * 0.09m) + ((totalGross-500m) * (0.001m * INPSRate));
                response.IncomeTax += inpstax;
            }
            response.NetAmount = GetNetValue(response);
        }

        private void CalculateTaxForGermany(HomePayCalculateRequest request, HomePayCalculateResponse response)
        {
            var totalGross = GetGross(request);
            response.GrossAmount = totalGross;
            if (totalGross > 400)
            {
                response.IncomeTax = (400m * 0.25m) + ((totalGross - 400m) * 0.32m);
            }
            response.Pension = totalGross * 0.02m;
            response.NetAmount = GetNetValue(response);
        }
    }
}