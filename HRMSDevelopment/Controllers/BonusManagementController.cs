using HRMSDevelopment.Common;
using HRMSDevelopment.Model.DBEntity;
using HRMSDevelopment.Model.DBEntity.ViewModel;
using HRMSDevelopment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;

namespace HRMSDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusManagementController : ControllerBase
    {
        private readonly IBaseService<BonusCriteria> _bonusCriteriaService;
        private readonly IBaseService<BonusDistribution> _bonusDistributionService;
        private readonly IBaseService<Employee> _employeeService;
        private readonly IBaseService<Bonus> _bonusService;
        private readonly IBaseService<Salary> _salaryService;
        private readonly IBaseService<EmploymentHistory> _employmentHistoryService;
        public BonusManagementController(IBaseService<BonusCriteria> bonusCriteriaService, IBaseService<BonusDistribution> bonusDistributionService, IBaseService<Employee> employeeService, IBaseService<Bonus> bonusService, IBaseService<Salary> salaryService, IBaseService<EmploymentHistory> employmentHistroyService)
        {
            _bonusCriteriaService = bonusCriteriaService;
            _bonusDistributionService = bonusDistributionService;
            _employeeService = employeeService;
            _bonusService = bonusService;
            _salaryService = salaryService;
            _employmentHistoryService = employmentHistroyService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<List<BonusCriteria>>), 200)]
        [Route("criteria")]
        public async Task<IActionResult> GetAllEmployeesBonusCriteria()
        {
            var response = new PayloadResponse<List<BonusCriteria>>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.GetAllData
            };

            var employeesBonusCriteria = await _bonusCriteriaService.GetAll();

            if (employeesBonusCriteria == null)
            {
                response.message = new List<string>() { "No bonus criteria found" };
                return NotFound(response);
            }

            response.success = true;
            response.message = new List<string>() { "Found all Bonus Criteria" };
            response.payload = employeesBonusCriteria.ToList();
            return Ok(response);
        }

        [Authorize(Roles = "HR")]
        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("criteria")]
        public async Task<IActionResult> AddEmployeeBonusCriteria(BonusCriteriaViewModel model)
        {
            var response = new PayloadResponse<BonusCriteria>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save

            };

            var newCriteria = new BonusCriteria()
            {
                CriteriaName = model.criteria_name,
                CriteriaDescription = model.criteria_description,
                ValidFrom = model.valid_from,
                MaximumBonusAmount = model.maximum_bonusAmount,
                MinimumBonusAmount = model.minimum_bonusAmount
            };

            OperationResult result = await _bonusCriteriaService.Add(newCriteria);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Bonus Criteria added successfully" };
                response.payload = newCriteria;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PayloadResponse<BonusCriteria>), 200)]
        [Route("criteria/{criteriaId:int}")]
        public async Task<IActionResult> UpdateEmployeeBonusCriteria(int criteriaId, BonusCriteriaViewModel model)
        {
            var response = new PayloadResponse<BonusCriteria>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Update

            };

            if (criteriaId <= 0)
            {
                response.message = new List<string>() { "Enter valid Id." };
                return BadRequest(response);
            }

            BonusCriteria oldData = await _bonusCriteriaService.GetById(criteriaId);

            if (oldData == null)
            {
                response.message = new List<string>() { "No bonus criteria found." };
                return NotFound(response);
            }

            oldData.CriteriaName = model.criteria_name;
            oldData.CriteriaDescription = model.criteria_description;
            oldData.ValidFrom = model.valid_from;

            OperationResult result = await _bonusCriteriaService.Update(oldData);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Bonus criteria updated successfully" };
                response.payload = result.Result;
                return Ok(response);
            }

            return BadRequest(response);

        }

        [Authorize(Roles = "HR")]
        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("distribute")]
        public async Task<IActionResult> DistributeEmployeeBonus(BonusDistributionViewModel model)
        {
            var response = new PayloadResponse<BonusDistribution>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save
            };

            var salaryList = await _salaryService.FindByCondition(s=> s.EmployeeId == model.employee_id);
            var salary = salaryList.FirstOrDefault();

            if (salary == null)
            {
                response.message = new List<string> { "Salary record not found." };
                return BadRequest(response);
            }

            var employmentHistoryList = await _employmentHistoryService.FindByCondition(h=> h.EmployeeId == model.employee_id);
            var employmentHistory = employmentHistoryList.FirstOrDefault();

            if (employmentHistory == null)
            {
                response.message = new List<string> { "Employment history not found." };
                return BadRequest(response);
            }

            var bonusCriteria = await _bonusCriteriaService.GetById(model.bonus_criteriaId);

            bool isEligible = CheckBonusEligibility(salary, employmentHistory);

            decimal bonusAmount = CalculateBonusAmount(salary, bonusCriteria);

            var newDistribution = new BonusDistribution()
            {
                BonusCriteriaId = model.bonus_criteriaId,
                EmployeeId = model.employee_id,
                BonusAmount = bonusAmount,
                DistributionDate = DateTime.Now
            };

            OperationResult result = await _bonusDistributionService.Add(newDistribution);

            if (result.Success)
            {
                var newBonus = new Bonus()
                {
                    SalaryId = salary.SalaryId,
                    BonusAmmount = bonusAmount,
                    BonusDate = DateTime.Now
                };

                OperationResult bonusResult = await _bonusService.Add(newBonus);

            }

            response.success = true;
            response.payload = newDistribution;
            response.message = new List<string>() { "Bonuses distributed successfully." };
            return Ok(response);

        }


        private bool CheckBonusEligibility(Salary salary, EmploymentHistory employmentHistory)
        {
           
            var serviceDuration = DateTime.Now - employmentHistory.StartDate;

            if (serviceDuration.TotalDays < 50)
            {
                return false;
            }

            if (salary.BasicSalary < 40000)
            {
                return false;
            }

            return true;
        }

        private decimal CalculateBonusAmount(Salary salary, BonusCriteria bonusCriteria )
        {
            decimal bonusAmount = 0;

            
            if (bonusCriteria.CriteriaName == "PerformanceBonus")
            {
                bonusAmount += salary.BasicSalary * (decimal)0.1;
            }

            if (bonusCriteria.CriteriaName == "AttendanceBonus")
            {
                bonusAmount += salary.BasicSalary * (decimal)0.08;
            }

            if (bonusCriteria.CriteriaName == "ProfitSharingBonus")
            {
                bonusAmount += salary.BasicSalary * (decimal)0.07;
            }

            if (bonusAmount > bonusCriteria.MaximumBonusAmount)
            {
                bonusAmount = bonusCriteria.MaximumBonusAmount;
            }

            return bonusAmount;
        }

    }
}
