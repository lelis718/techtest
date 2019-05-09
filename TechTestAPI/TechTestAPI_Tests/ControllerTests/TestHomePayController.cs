using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTestAPI.Controllers;
using TechTestAPI.ViewModels;
using Xunit;

namespace TechTestAPI_Tests.ControllerTests
{
    public class TestHomePayController
    {
        [Fact(DisplayName ="Should inform gross amount calculation for an employee's salary")]
        public void CalculateGrossAmout()
        {
            // Arrange
            var controller = new HomePayController();
            var employeeRequest = new HomePayCalculateRequest();
            employeeRequest.HourlyRate = 10.0m;
            employeeRequest.HoursWorked = 40;
            employeeRequest.EmployeeLocation = "Ireland";

            // Act
            var response = controller.CalculateGrossAmount(employeeRequest);
            response.Wait();
            var result = response.Result;

            // Assert
            var objResult = result as OkObjectResult;
            Assert.NotNull(objResult);

            var homepayResponse = objResult.Value as HomePayCalculateResponse;
            Assert.NotNull(homepayResponse);
            Assert.True(homepayResponse.EmployeeLocation == "Ireland");
            Assert.True(homepayResponse.GrossAmount == 400.0m);
        }

        [Fact(DisplayName = "Should see deductions charged for employees in Ireland")]
        public void CalculateDeductionsIreland()
        {
            // Arrange
            var controller = new HomePayController();
            var employeeRequest = new HomePayCalculateRequest();
            employeeRequest.HourlyRate = 50.0m;
            employeeRequest.HoursWorked = 40;
            employeeRequest.EmployeeLocation = "Ireland";

            // Act
            var response = controller.CalculateGrossAmount(employeeRequest);
            response.Wait();
            var result = response.Result;

            // Assert
            var objResult = result as OkObjectResult;
            Assert.NotNull(objResult);

            var homepayResponse = objResult.Value as HomePayCalculateResponse;
            Assert.NotNull(homepayResponse);
            Assert.True(homepayResponse.EmployeeLocation == "Ireland");
            Assert.True(homepayResponse.GrossAmount == 2000.0m);
            Assert.True(homepayResponse.IncomeTax == 710.0m);
            Assert.True(homepayResponse.UniversalSocialCharge == 155.0m);
            Assert.True(homepayResponse.Pension == 80.0m);
            Assert.True(homepayResponse.NetAmount == 1055.0m); //2000.0m - 710.0m - 155.0m - 80.0m

        }

        [Fact(DisplayName = "Should see deductions charged for employees in Italy")]
        public void CalculateDeductionsItaly()
        {
            // Arrange
            var controller = new HomePayController();
            var employeeRequest = new HomePayCalculateRequest();
            employeeRequest.HourlyRate = 50.0m;
            employeeRequest.HoursWorked = 40;
            employeeRequest.EmployeeLocation = "Italy";

            // Act
            var response = controller.CalculateGrossAmount(employeeRequest);
            response.Wait();
            var result = response.Result;

            // Assert
            var objResult = result as OkObjectResult;
            Assert.NotNull(objResult);

            var homepayResponse = objResult.Value as HomePayCalculateResponse;
            Assert.NotNull(homepayResponse);
            Assert.True(homepayResponse.EmployeeLocation == "Italy");
            Assert.True(homepayResponse.GrossAmount == 2000.0m);
            Assert.True(homepayResponse.IncomeTax == (567.50m) ); //500.0m + 67.50m - Income Tax and INPS Contribution
            Assert.True(homepayResponse.UniversalSocialCharge == 0m);
            Assert.True(homepayResponse.Pension == 0m);
            Assert.True(homepayResponse.NetAmount == 1432.50m); //2000.0m - 567.50m

        }

        [Fact(DisplayName = "Should see deductions charged for employees in Germany")]
        public void CalculateDeductionsGermany()
        {
            // Arrange
            var controller = new HomePayController();
            var employeeRequest = new HomePayCalculateRequest();
            employeeRequest.HourlyRate = 50.0m;
            employeeRequest.HoursWorked = 40;
            employeeRequest.EmployeeLocation = "Germany";

            // Act
            var response = controller.CalculateGrossAmount(employeeRequest);
            response.Wait();
            var result = response.Result;

            // Assert
            var objResult = result as OkObjectResult;
            Assert.NotNull(objResult);

            var homepayResponse = objResult.Value as HomePayCalculateResponse;
            Assert.NotNull(homepayResponse);
            Assert.True(homepayResponse.EmployeeLocation == "Germany");
            Assert.True(homepayResponse.GrossAmount == 2000.0m);
            Assert.True(homepayResponse.IncomeTax == (612.0m)); 
            Assert.True(homepayResponse.UniversalSocialCharge == 0m);
            Assert.True(homepayResponse.Pension == 40.0m);
            Assert.True(homepayResponse.NetAmount == 1348.0m); //2000.0m - 612.0m - 40.0m

        }

    }
}
