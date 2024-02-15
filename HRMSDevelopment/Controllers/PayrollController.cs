using HRMSDevelopment.Common;
using HRMSDevelopment.Model.DBEntity;
using HRMSDevelopment.Model.DBEntity.ViewModel;
using HRMSDevelopment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Resources;

namespace HRMSDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IBaseService<Employee> _employeeService;
        private readonly IBaseService<Salary> _salaryService;
        private readonly IBaseService<Deduction> _deductionService;
        private readonly IBaseService<Bonus> _bonusService;
        private readonly IBaseService<Tax> _taxService;
        private readonly IBaseService<PayrollRecord> _payrollRecordService;

        public PayrollController(IBaseService<Employee> employeeService, IBaseService<Salary> salaryService, IBaseService<Deduction> deductionService, IBaseService<Bonus> bonusService, IBaseService<Tax> taxService, IBaseService<PayrollRecord> payrollrecordService)
        {
            _employeeService = employeeService;
            _salaryService = salaryService;
            _deductionService = deductionService;
            _bonusService = bonusService;
            _taxService = taxService;
            _payrollRecordService = payrollrecordService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("generate")]
        public async Task<IActionResult> GenerateMonthlyPayroll([FromBody] PayrollPeriod payrollPeriod)
        {
            var response = new PayloadResponse<List<PayrollRecord>>
            {
                success = false,
                message = new List<string>() { "" },
                payload = new List<PayrollRecord>(),
                operation_type = PayloadType.Save
            };

            var employees = await _employeeService.GetAll();

            foreach (var employee in employees)
            {

                var salary = await _salaryService.FindByCondition(s => s.EmployeeId == employee.EmployeeId);

                if (salary != null && salary.Any())
                {

                    var deductions = await _deductionService.FindByCondition(d => d.SalaryId == salary.FirstOrDefault().SalaryId && d.DeductionDate.Month == payrollPeriod.Month && d.DeductionDate.Year == payrollPeriod.Year);

                    var bonuses = await _bonusService.FindByCondition(b => b.SalaryId == salary.FirstOrDefault().SalaryId && b.BonusDate.Month == payrollPeriod.Month && b.BonusDate.Year == payrollPeriod.Year);

                    var applicableTax = await _taxService.FindByCondition(t => salary.FirstOrDefault().BasicSalary >= t.LowerBound && salary.FirstOrDefault().BasicSalary <= t.UpperBound);

                    var taxAmount = applicableTax != null ? salary.FirstOrDefault().BasicSalary * applicableTax.FirstOrDefault().Rate : 0;

                    var totalDeduction = deductions.Sum(d => d.DeductionAmmount);

                    var totalBonus = bonuses.Sum(b => b.BonusAmmount);

                    var netPay = salary.FirstOrDefault().BasicSalary - totalDeduction - taxAmount + totalBonus;

                    var payroll = new PayrollRecord()
                    {
                        EmployeeId = employee.EmployeeId,
                        GrossPay = salary.FirstOrDefault().BasicSalary,
                        TotalDeductions = totalDeduction,
                        TotalBonuses = totalBonus,
                        NetPay = netPay
                    };

                    OperationResult result = await _payrollRecordService.Add(payroll);

                    response.payload.Add(payroll);
                }

                else
                {
                    response.message = new List<string> { "No salary record found." };

                    return BadRequest(response);
                }
            }

            if(response.payload.Any())
            {
                response.success = true;
                response.message = new List<string>() { "Payroll generated successfully." };
                return Ok(response);
            }

          
            response.message = new List<string>() { "No payroll records generated." };
            return StatusCode(500, response);
                
        }

        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("taxrate")]
        public async Task<IActionResult> AddTaxRate(Tax model)
        {
            var response = new PayloadResponse<Tax>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save

            };

            var newTaxRate = new Tax()
            {
               LowerBound = model.LowerBound,
               UpperBound = model.UpperBound,
               Rate = model.Rate
            };

            OperationResult result = await _taxService.Add(newTaxRate);

            if (result.Success)
            {
                response.success = true;
                response.message = new List<string>() { "Tax rate added successfully" };
                response.payload = newTaxRate;
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
