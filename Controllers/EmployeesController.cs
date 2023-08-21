using EmployeeSampleWebApiWithAzureAD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSampleWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>()
            {
                new Employee
                {
                    Id = 1,
                    Name ="Test",
                },
            };

            return employees;
        }
    }
}
