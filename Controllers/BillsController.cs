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
    public class BillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillModel>>> GetBill_Details()
        {
            return await _context.Bill_Details.ToListAsync();
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillModel>> GetBillModel(int id)
        {
            var billModel = await _context.Bill_Details.FindAsync(id);

            if (billModel == null)
            {
                return NotFound();
            }

            return billModel;
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillModel(int id, BillModel billModel)
        {
            if (id != billModel.BillId)
            {
                return BadRequest();
            }

            _context.Entry(billModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillModelExists(id))
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

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BillModel>> PostBillModel(BillModel billModel)
        {
            try {
                billModel.BillTime = DateTime.Now;
                _context.Bill_Details.Add(billModel);
                //await _context.SaveChangesAsync();

                return billModel;
            }
            catch (Exception e) {
                return BadRequest(e); }
            
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillModel(int id)
        {
            var billModel = await _context.Bill_Details.FindAsync(id);
            if (billModel == null)
            {
                return NotFound();
            }

            _context.Bill_Details.Remove(billModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillModelExists(int id)
        {
            return _context.Bill_Details.Any(e => e.BillId == id);
        }
    }
}
