using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAppAPI.DataSet;
using StoreAppAPI.Model;

namespace StoreAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployee_Details()
        {
            try
            {
                return await _context.Employee_Details.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployeeModel(int id)
        {
            try
            {
                var employeeModel = await _context.Employee_Details.FindAsync(id);

                if (employeeModel == null)
                {
                    return NotFound("Employee not found");
                }

                return employeeModel;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeModel(int id, EmployeeModel employeeModel)
        {
            if (id != employeeModel.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employeeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> PostEmployeeModel(EmployeeModel employeeModel)
        {
            try
            {
                var duplicate = (from d in _context.Employee_Details where d.Phone == employeeModel.Phone select d).ToList();
                if (duplicate.Count > 0)
                {
                    return BadRequest("Employee already present");
                }
                else
                {
                    _context.Employee_Details.Add(employeeModel);
                    await _context.SaveChangesAsync();
                    return Ok(employeeModel);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Employees/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmployeeModel(int id)
        //{
        //    var employeeModel = await _context.Employee_Details.FindAsync(id);
        //    if (employeeModel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Employee_Details.Remove(employeeModel);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool EmployeeModelExists(int id)
        {
            return _context.Employee_Details.Any(e => e.EmployeeId == id);
        }
    }
}
