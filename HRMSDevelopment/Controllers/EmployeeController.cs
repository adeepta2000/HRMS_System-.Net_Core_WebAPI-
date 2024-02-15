using HRMSDevelopment.Common;
using HRMSDevelopment.Model.DBEntity;
using HRMSDevelopment.Model.DBEntity.ViewModel;
using HRMSDevelopment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMSDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IBaseService<Employee> _employeeService;
        public EmployeeController(IBaseService<Employee> employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<Employee>), 200)]
        [Route("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var response = new PayloadResponse<Employee>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.GetById

            };

            if (id <= 0)
            {
                response.message = new List<string>() { "Please enter valid id." };
                return BadRequest(response);
            }

            Employee data = await _employeeService.GetById(id);

            if (data == null)
            {
                response.message = new List<string>() { "No Employee found." };
                return NotFound(response);
            }

            response.success = true;
            response.message = new List<string>() { "Found the Employee." };
            response.payload = data;
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<List<Employee>>), 200)]
        [Route("")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = new PayloadResponse<List<Employee>>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.GetAllData
            };

            var employees = await _employeeService.GetAll();

            if (employees == null)
            {
                response.message = new List<string>() { "No Employee List found" };
                return NotFound(response);
            }

            response.success = true;
            response.message = new List<string>() { "Found all Employees" };
            response.payload = employees.ToList();
            return Ok(response);
        }

        [Authorize(Roles = "HR")]
        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("")]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel model)
        {
            var response = new PayloadResponse<Employee>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save
             
            };

            var newEmployee = new Employee()
            {
                Name = model.name,
                Email = model.email,
                MobileNumber = model.mobile_number,
                Position = model.position,
                Department = model.department,
                AccountNumber = model.account_number,
                EmploymentStatus = model.employment_status,
                Username = model.username,
                Password = model.password
            };

            OperationResult result = await _employeeService.Add(newEmployee);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Employee added successfully" };
                response.payload = newEmployee;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = "HR")]
        [HttpPut]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<Employee>), 200)]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeViewModel model)
        {
            var response = new PayloadResponse<Employee>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Update

            };

            if (id <= 0)
            {
                response.message = new List<string>() { "Enter valid Id." };
                return BadRequest(response);
            }

            Employee oldData = await _employeeService.GetById(id);

            if (oldData == null)
            {
                response.message = new List<string>() { "No Employee found." };
                return NotFound(response);
            }

            oldData.Name = model.name;
            oldData.Email = model.email;
            oldData.MobileNumber = model.mobile_number;
            oldData.Position = model.position;
            oldData.Department = model.department;
            oldData.AccountNumber = model.account_number;
            oldData.EmploymentStatus = model.employment_status;
            oldData.Username = model.username;
            oldData.Password = model.password;

            OperationResult result = await _employeeService.Update(oldData);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Employee updated successfully" };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);

        }

        [Authorize(Roles = "HR")]
        [HttpDelete]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<Employee>), 200)]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var response = new PayloadResponse<Employee>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Delete
            };

            if (id == 0)
            {
                response.message = new List<string>() { "Enter valid Id." };
                return BadRequest(response);
            }

            OperationResult result = _employeeService.Delete(id);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Employee Successfully deleted." };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<List<Employee>>), 200)]
        [Route("search")]
        public async Task<IActionResult> SearchEmployees(string keyword)
        {
            var response = new PayloadResponse<List<Employee>>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.GetByCondition
            };

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(response);
            }

            string sanitizedKeyword = keyword.Replace(" ", "");

            var employees = await _employeeService.FindByCondition(x => x.Name.Replace(" ", "").Contains(sanitizedKeyword) || x.Username.Replace(" ", "").Contains(sanitizedKeyword)
            || x.Email.Replace(" ", "").Contains(sanitizedKeyword) || x.MobileNumber.Replace(" ", "").Contains(sanitizedKeyword) || x.Position.Replace(" ", "").Contains(sanitizedKeyword) ||
            x.Department.Replace(" ", "").Contains(sanitizedKeyword));

            if (employees != null)
            {
                response.success = true;
                response.message = new List<string>() { "Found result by condition." };
                response.payload = employees.ToList();
                return Ok(response);
            }
            else
            {
                response.message = new List<string>() { "No matching records found." };
                return NotFound(response);
            }
        }

        [HttpGet]
        [Route("address")]
        public async Task<IActionResult> GetAddress(string address)
        {
            var response = new PayloadResponse<List<Employee>>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.GetByCondition
            };

            var empployee = await _employeeService.FindByCondition(e => e.Address.Contains(address), e=> e.Salaries) ;

            var allEmployee = empployee.ToList();

            if (empployee != null)
            {
                response.message = new List<string>() { "Get all employee." };
                response.payload = allEmployee;
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
