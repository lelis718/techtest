using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTestAPI.ViewModels
{
    public class HomePayCalculateRequest
    {
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public String EmployeeLocation { get; set; }
    }
}
