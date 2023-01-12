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
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Login
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<LoginModel>>> GetLogin_Details()
        //{
        //    return await _context.Login_Details.ToListAsync();
        //}

        //// GET: api/Login/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<LoginModel>> GetLoginModel(string id)
        //{
        //    var loginModel = await _context.Login_Details.FindAsync(id);

        //    if (loginModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return loginModel;
        //}

        // PUT: api/Login/5
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult> PutLoginModel(LoginModel loginmodel)
        {
            try {
                var user = await _context.Login_Details.FindAsync(loginmodel.UserName);
            }
            catch(Exception ex) {
                return BadRequest(ex);
            }
            return NoContent();
        }

        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoginModel>> PostLoginModel(LoginModel loginModel)
        {
            _context.Login_Details.Add(loginModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoginModelExists(loginModel.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(loginModel.UserName);
        }

        // DELETE: api/Login/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLoginModel(string id)
        //{
        //    var loginModel = await _context.Login_Details.FindAsync(id);
        //    if (loginModel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Login_Details.Remove(loginModel);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool LoginModelExists(string id)
        {
            return _context.Login_Details.Any(e => e.UserName == id);
        }
    }
}
