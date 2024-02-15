using HRMSDevelopment.Common;
using HRMSDevelopment.Common.Enum;
using HRMSDevelopment.Model.DBEntity;
using HRMSDevelopment.Model.DBEntity.ViewModel;
using HRMSDevelopment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace HRMSDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmploymentHistoryController : ControllerBase
    {
        private readonly IBaseService<EmploymentHistory> _employmentHistoryService;
        public EmploymentHistoryController(IBaseService<EmploymentHistory> employmentHistoryService)
        {
            _employmentHistoryService = employmentHistoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<List<EmploymentHistory>>), 200)]
        [Route("")]
        public async Task<IActionResult> GetAllEmployeesEmploymentHistory()
        {
            var response = new PayloadResponse<List<EmploymentHistory>>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.GetAllData
            };

            var employmentHistory = await _employmentHistoryService.GetAll();

            if (employmentHistory == null)
            {
                response.message = new List<string>() { "No Employees Employment History Found" };
                return NotFound(response);
            }

            response.success = true;
            response.message = new List<string>() { "Found all Employees Employment History" };
            response.payload = employmentHistory.ToList();
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<List<EmploymentHistory>>), 200)]
        [Route("{employeeId:int}")]
        public async Task<IActionResult> GetEmploymentHistoryForEmployee(int employeeId)
        {
            var response = new PayloadResponse<List<EmploymentHistory>>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.GetAllData
            };

            var employeeHistory = await _employmentHistoryService.FindByCondition(e => e.EmployeeId == employeeId);

            if (employeeHistory == null)
            {
                response.message = new List<string>() { "No history found for the employee" };
                return NotFound(response);
            }

            response.success = true;
            response.message = new List<string>() { "Found employment history of the employee." };
            response.payload = employeeHistory.ToList();
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("")]
        public async Task<IActionResult> AddEmployeeEmploymentHistory(EmploymentHistoryViewModel model)
        {
            var response = new PayloadResponse<EmploymentHistory>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save

            };

            var newEmploymentHistory = new EmploymentHistory()
            {
                EmployeeId = model.employee_id,
                StartDate = model.start_date,
                EndDate = model.end_date,
                JobTitle = model.job_title,
                Department = model.department,
                Salary = model.salary,
                EmploymentAction = model.employment_action,
                ActionDate = DateTime.Now,
                Comments = model.comments
            };

            OperationResult result = await _employmentHistoryService.Add(newEmploymentHistory);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Employee employment history added successfully" };
                response.payload = newEmploymentHistory;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<EmploymentHistory>), 200)]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEmployeeEmploymentHistory(int id, EmploymentHistoryViewModel model)
        {
            var response = new PayloadResponse<EmploymentHistory>
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

            EmploymentHistory oldData = await _employmentHistoryService.GetById(id);

            if (oldData == null)
            {
                response.message = new List<string>() { "No Employee employment history found." };
                return NotFound(response);
            }

            oldData.EmployeeId = model.employee_id;
            oldData.StartDate = model.start_date;
            oldData.EndDate = model.end_date;
            oldData.JobTitle = model.job_title;
            oldData.Department = model.department;
            oldData.Salary = model.salary;
            oldData.EmploymentAction = model.employment_action;
            oldData.ActionDate = DateTime.Now;
            oldData.Comments = model.comments;

            OperationResult result = await _employmentHistoryService.Update(oldData);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Employment history updated successfully" };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);

        }

        [HttpDelete]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<EmploymentHistory>), 200)]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteEmployeeEmploymentHistory(int id)
        {
            var response = new PayloadResponse<EmploymentHistory>
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

            OperationResult result = _employmentHistoryService.Delete(id);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Employment history Successfully deleted." };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
