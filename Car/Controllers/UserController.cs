#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Car.Models;
using Car.Entities;
using Car.Helpers;

namespace Car.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly carContext _context;

        public UserController(carContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var user = from u in _context.Users
                           select new
                           {
                               Userid = u.Userid,
                               Useremail = u.Useremail,
                               Username = u.Username,
                               UserCity = u.UserCity,
                               Userphno = u.Userphno,
                               UserRole = u.UserRole
                           };
                return Ok(new { status = "success", data = await user.ToListAsync(), message = "Get All Users Successful" });
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { status = "failed", serverMessage = e.Message, message = "Get All Users Failed" });
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Userid == id).FirstAsync();

                if (user == null)
                {
                    return NotFound(new { status = "failed", message = "User Not Found" });
                }

                return Ok(
                    new
                    {
                        status = "success",
                        data = new
                        {
                            Userid = user.Userid,
                            Useremail = user.Useremail,
                            Username = user.Username,
                            UserCity = user.UserCity,
                            Userphno = user.Userphno,
                            UserRole = user.UserRole
                        },
                        message = "Get User By Id Successful"
                    }
                );
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                return NotFound(new { status = "failed", serverMessage = e.Message, message = "User Not Found" });
            }
        }

        // PUT: api/User/5  
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserById(Guid id, User user)
        {
            if (id != user.Userid)
            {
                return BadRequest();
            }
            user.Userpassword = BCrypt.Net.BCrypt.HashPassword(user.Userpassword);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!IsUserExists(id))
                {
                    return NotFound(new { status = "failed", message = "No User Found" });
                }
                else
                {
                    Console.WriteLine(ex);
                    return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "Update User By Id Failed" });
                }
            }
            return Ok(new { status = "success", message = "Details Updated" });
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> RegisterNewUser(User user)
        {
            try
            {
                user.Userpassword = BCrypt.Net.BCrypt.HashPassword(user.Userpassword);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUserById", new { id = user.Userid }, new { status = "success", data = user, message = "User registration Successful" });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "User registration Failed" });
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(new { status = "success", message = "Deleted User" });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "User Delete Failed" });
            }
        }

        private bool IsUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Userid == id);
        }
    }
}
