using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechTestAPI.Domain;
using TechTestAPI.Domain.TaxProviders;
using TechTestAPI.ViewModels;

namespace TechTestAPI.Controllers
{
    [Route("api/home-pay")]
    [ApiController]
    public class HomePayController : ControllerBase
    {
        private IList<ITaxProvider> taxProviders;

        public HomePayController()
        {
            //Maybe refactor this in other iteration (Inject in the controller)
            var list = new List<ITaxProvider>();
            list.Add(new IrelandTaxProvider());
            list.Add(new ItalyTaxProvider());
            list.Add(new GermanyTaxProvider());

            this.taxProviders = list;

        }


        [AcceptVerbs("Post")]
        [Route("calculate-gross-amount")]
        public async Task<IActionResult> CalculateGrossAmount(HomePayCalculateRequest employeeRequest)
        {
            validateRequest(employeeRequest);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var country = employeeRequest.EmployeeLocation;
            var selectedProvider = this.taxProviders.FirstOrDefault(i => i.Country.Equals(country,StringComparison.InvariantCultureIgnoreCase));
            if(selectedProvider == null)
            {
                return BadRequest("Invalid Country");
            }

            var response = selectedProvider.CalculateHomePay(employeeRequest);

            return Ok(response);
        }

        private void validateRequest(HomePayCalculateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EmployeeLocation))
            {
                ModelState.AddModelError("EmployeeLocation", "Location is mandatory");
            }
        }

    }
}