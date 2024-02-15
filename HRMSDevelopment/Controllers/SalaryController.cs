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
    public class SalaryController : ControllerBase
    {
        private readonly IBaseService<Salary> _salaryService;
        private readonly IBaseService<SalaryRevision> _salaryRevisionService;
        private readonly IBaseService<Deduction> _deductionService;
        private readonly IBaseService<Bonus> _bonusService;
        public SalaryController(IBaseService<Salary> salaryService, IBaseService<SalaryRevision> salaryRevisionService, IBaseService<Deduction> deductionService, IBaseService<Bonus> bonusService)
        {
            _salaryService = salaryService;
            _salaryRevisionService = salaryRevisionService;
            _deductionService = deductionService;
            _bonusService = bonusService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<List<Salary>>), 200)]
        [Route("")]
        public async Task<IActionResult> GetAllEmployeesSalary()
        {
            var response = new PayloadResponse<List<Salary>>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.GetAllData
            };

            var employeesSalary = await _salaryService.GetAll();

            if (employeesSalary == null)
            {
                response.message = new List<string>() { "No Salary List found" };
                return NotFound(response);
            }

            response.success = true;
            response.message = new List<string>() { "Found all Employees Salary" };
            response.payload = employeesSalary.ToList();
            return Ok(response);
        }

        [Authorize(Roles = "HR")]
        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("")]
        public async Task<IActionResult> AddEmployeeSalary(SalaryViewModel model)
        {
            var response = new PayloadResponse<Salary>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save

            };

            var newSalary = new Salary()
            {
               EmployeeId = model.employee_id,
               BasicSalary = model.basic_salary
            };

            OperationResult result = await _salaryService.Add(newSalary);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Salary added successfully" };
                response.payload = newSalary;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = "HR")]
        [HttpPut]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<Salary>), 200)]
        [Route("{salaryId:int}")]
        public async Task<IActionResult> UpdateEmployeeSalary(int salaryId, SalaryViewModel model)
        {
            var response = new PayloadResponse<Salary>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Update

            };

            if (salaryId <= 0)
            {
                response.message = new List<string>() { "No salary record found." };
                return BadRequest(response);
            }

            Salary oldData = await _salaryService.GetById(salaryId);

            if (oldData == null)
            {
                response.message = new List<string>() { "No salary record found." };
                return NotFound(response);
            }

            oldData.BasicSalary = model.basic_salary;

            OperationResult result = await _salaryService.Update(oldData);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Salary updated successfully" };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);

        }

        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("revision")]
        public async Task<IActionResult> AddEmployeeSalaryRevision(SalaryRevisionViewModel model)
        {
            var response = new PayloadResponse<SalaryRevision>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save

            };

            var newSalaryRevision = new SalaryRevision()
            {
                SalaryId = model.salary_id,
                RevisedSalary = model.revised_salary,
                RevisionDate = DateTime.Now
            };

            OperationResult addRevisionResult = await _salaryRevisionService.Add(newSalaryRevision);

            if (!addRevisionResult.Success)
            {
                response.message = new List<string>() { "Failed to add salary revision." };
                return BadRequest(response);
            }

            var salaryRecord = await _salaryService.GetById(model.salary_id);

            if (salaryRecord == null)
            {
                response.message = new List<string>() { "Salary record not found." };
                return NotFound(response);
            }

            salaryRecord.BasicSalary = model.revised_salary;

            OperationResult updateSalaryResult = await _salaryService.Update(salaryRecord);

            if (!updateSalaryResult.Success)
            {
                response.message = new List<string>() { "Failed to update salary record." };
                return BadRequest(response);
            }

            response.success = true;
            response.message = new List<string>() { "Salary revised successfully" };
            response.payload = newSalaryRevision;
            return Ok(response);

        }

        [HttpPut]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<SalaryRevision>), 200)]
        [Route("revision/{revisionId:int}")]
        public async Task<IActionResult> UpdateEmployeeSalaryRevision(int revisionId, SalaryRevisionViewModel model)
        {
            var response = new PayloadResponse<SalaryRevision>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Update

            };

            if (revisionId <= 0)
            {
                response.message = new List<string>() { "No salary revision record found." };
                return BadRequest(response);
            }

            SalaryRevision oldData = await _salaryRevisionService.GetById(revisionId);

            if (oldData == null)
            {
                response.message = new List<string>() { "No salary revision record found." };
                return NotFound(response);
            }

            oldData.RevisedSalary = model.revised_salary;
            oldData.RevisionDate = DateTime.Now;

            OperationResult result = await _salaryRevisionService.Update(oldData);

            if (!result.Success)
            {
                response.message = new List<string>() { "Failed to update salary revision." };
                return BadRequest(response);
            }

            var salaryRecord = await _salaryService.GetById(model.salary_id);

            if (salaryRecord == null)
            {
                response.message = new List<string>() { "Salary record not found." };
                return NotFound(response);
            }

            salaryRecord.BasicSalary = model.revised_salary;

            OperationResult updateSalaryResult = await _salaryService.Update(salaryRecord);

            if (!updateSalaryResult.Success)
            {
                response.message = new List<string>() { "Failed to update salary record." };
                return BadRequest(response);
            }

            response.success = true;
            response.message = new List<string>() { "Salary revised successfully" };
            response.payload = result.Result;
            return Ok(response);

        }

        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("deduction")]
        public async Task<IActionResult> AddEmployeeSalaryDeduction(DeductionViewModel model)
        {
            var response = new PayloadResponse<Deduction>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save

            };

            var newDeduction = new Deduction()
            {
                SalaryId = model.salary_id,
                DeductionAmmount = model.deduction_ammount,
                DeductionDate = DateTime.Now
            };

            OperationResult result = await _deductionService.Add(newDeduction);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Salary deduction added successfully" };
                response.payload = newDeduction;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<Deduction>), 200)]
        [Route("deduction/{deductionId:int}")]
        public async Task<IActionResult> UpdateEmployeeSalaryDeduction(int deductionId, DeductionViewModel model)
        {
            var response = new PayloadResponse<Deduction>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Update

            };

            if (deductionId <= 0)
            {
                response.message = new List<string>() { "No salary deduction record found." };
                return BadRequest(response);
            }

            Deduction oldData = await _deductionService.GetById(deductionId);

            if (oldData == null)
            {
                response.message = new List<string>() { "No salary deduction record found." };
                return NotFound(response);
            }

            oldData.DeductionAmmount = model.deduction_ammount;
            oldData.DeductionDate = DateTime.Now;

            OperationResult result = await _deductionService.Update(oldData);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Salary deduction updated successfully" };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);

        }

        [HttpDelete]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<Deduction>), 200)]
        [Route("deduction/{deductionId:int}")]
        public async Task<IActionResult> DeleteEmployeeSalaryDeduction(int deductionId)
        {
            var response = new PayloadResponse<Deduction>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Delete
            };

            if (deductionId <= 0)
            {
                response.message = new List<string>() { "Enter valid Id." };
                return BadRequest(response);
            }

            OperationResult result = _deductionService.Delete(deductionId);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Salar deduction Successfully deleted." };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("bonus")]
        public async Task<IActionResult> AddEmployeeSalaryBonus(BonusViewModel model)
        {
            var response = new PayloadResponse<Bonus>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save

            };

            var newBonus = new Bonus()
            {
                SalaryId = model.salary_id,
                BonusAmmount = model.bonus_ammount,
                BonusDate = DateTime.Now
            };

            OperationResult result = await _bonusService.Add(newBonus);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Salary bonus added successfully" };
                response.payload = newBonus;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<Deduction>), 200)]
        [Route("bonus/{bonusId:int}")]
        public async Task<IActionResult> UpdateEmployeeSalaryBonus(int bonusId, BonusViewModel model)
        {
            var response = new PayloadResponse<Bonus>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Update

            };

            if (bonusId <= 0)
            {
                response.message = new List<string>() { "No salary bonus record found." };
                return BadRequest(response);
            }

            Bonus oldData = await _bonusService.GetById(bonusId);

            if (oldData == null)
            {
                response.message = new List<string>() { "No salary bonus record found." };
                return NotFound(response);
            }

            oldData.BonusAmmount = model.bonus_ammount;
            oldData.BonusDate = DateTime.Now;

            OperationResult result = await _bonusService.Update(oldData);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Salary bonus updated successfully" };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);

        }

        [HttpDelete]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<Bonus>), 200)]
        [Route("bonus/{bonusId:int}")]
        public async Task<IActionResult> DeleteEmployeeSalaryBonus(int bonusId)
        {
            var response = new PayloadResponse<Bonus>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Delete
            };

            if (bonusId <= 0)
            {
                response.message = new List<string>() { "Enter valid Id." };
                return BadRequest(response);
            }

            OperationResult result = _bonusService.Delete(bonusId);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Salar bonus Successfully deleted." };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
