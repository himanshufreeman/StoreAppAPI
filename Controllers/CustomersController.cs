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
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomer_Details()
        {
            try
            {
                return await _context.Customer_Details.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerModel(int id)
        {
            var customerModel = await _context.Customer_Details.FindAsync(id);

            if (customerModel == null)
            {
                return NotFound();
            }

            return customerModel;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCustomerModel(int id, CustomerModel customerModel)
        //{
        //    //var input = customerModel.CustomerPhone;
        //    //var duplicate = (from d in _context.Customer_Details where d.CustomerPhone== customerModel.CustomerPhone select d).ToList
        //    if (id != customerModel.CustomerId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(customerModel).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CustomerModelExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostCustomerModel(CustomerModel customerModel)
        {
            try
            {
                var duplicate = (from d in _context.Customer_Details where d.CustomerPhone == customerModel.CustomerPhone select d).ToList();
                if (duplicate.Count > 0)
                {
                    return BadRequest("Customer already present");
                }
                else
                {
                    _context.Customer_Details.Add(customerModel);
                    await _context.SaveChangesAsync();
                }
                return customerModel;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerModel(int id)
        {
            try
            {
                var customerModel = await _context.Customer_Details.FindAsync(id);
                if (customerModel == null)
                {
                    return NotFound("Customer Not Found");
                }
                else
                {
                    _context.Customer_Details.Remove(customerModel);
                    await _context.SaveChangesAsync();
                }
                return Ok(customerModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool CustomerModelExists(int id)
        {
            return _context.Customer_Details.Any(e => e.CustomerId == id);
        }
    }
}
