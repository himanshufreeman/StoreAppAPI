using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
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

        // PUT: api/Login
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<LoginModel>> PutLoginModel(LoginModel loginmodel)
        {
            try {
                var user = await _context.Login_Details.FindAsync(loginmodel.UserName);
                if (user == null) { return BadRequest("User not present"); }
                else if (user.Role != loginmodel.Role) { return BadRequest("User not present with this role "+loginmodel.Role); }
                else if(user.Password == loginmodel.Password) { 
                    var token = CreateToken(user);
                    return Ok(new { Token = token, UserName = user.UserName, Role = user.Role }) ; 
                }
                else { return BadRequest("Password wrong"); }
                
            }
            catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoginModel>> PostLoginModel(LoginModel loginmodel)
        {
            
            try
            {
                var user = (from d in _context.Login_Details where d.UserName == loginmodel.UserName select d).ToList(); ;
                if (user.Count > 0) { return BadRequest("User not present"); }
                else
                {
                    _context.Login_Details.Add(loginmodel);
                    await _context.SaveChangesAsync();
                    return Ok(loginmodel);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
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

        //private bool LoginModelExists(string id)
        //{
        //    return _context.Login_Details.Any(e => e.UserName == id);
        //}

        private string CreateToken(LoginModel _user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes("1234567890123456...");
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                            new Claim(ClaimTypes.Name,Convert.ToString(_user.UserName)),
                            new Claim(ClaimTypes.Role,Convert.ToString(_user.Role)),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var securityToken = tokenHandler.CreateToken(TokenDescriptor);
            return (tokenHandler.WriteToken(securityToken));
        }
    }
}
