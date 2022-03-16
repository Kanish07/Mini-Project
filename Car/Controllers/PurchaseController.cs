#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Car.Models;
using Car.Helpers;

namespace Car.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly carContext _context;

        public PurchaseController(carContext context)
        {
            _context = context;
        }

        // GET: api/Purchase
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetAllPurchases()
        {
            try
            {
                var purchase = await _context.Purchases.Include(c => c.Car).Include(c => c.Car.User).Include(u => u.User).ToListAsync();
                return Ok(new { status = "success", data = purchase, message = "Get All Users Successful" });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "Get All Purchases Failed" });
            }
        }

        // GET: api/Purchase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchaseById(Guid id)
        {
            try
            {
                var purchase = await _context.Purchases.FindAsync(id);

                if (purchase == null)
                {
                    return NotFound();
                }

                return Ok(new { status = "success", data = purchase, message = "Get Purchase by Id Successful" });
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                return NotFound(new { status = "failed", serverMessage = e.Message, message = "Purchase Not Found" });
            }
        }

        // PUT: api/Purchase/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase(Guid id, Purchase purchase)
        {
            if (id != purchase.Purchaseid)
            {
                return BadRequest();
            }

            _context.Entry(purchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PurchaseExists(id))
                {
                    return NotFound(new { status = "failed", message = "No Purchase Found" });
                }
                else
                {
                    Console.WriteLine(ex);
                    return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "Update User By Id Failed" });
                }
            }
            return Ok(new { status = "success", message = "Details Updated" });
        }

        // POST: api/Purchase
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Purchase>> RegisterNewPurchase(Purchase purchase)
        {
            try
            {
                _context.Purchases.Add(purchase);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPurchaseById", new { id = purchase.Purchaseid }, new { status = "success", data = purchase, message = "Purchase registration Successful" });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "Purchase registration Failed" });
            }
        }

        // DELETE: api/Purchase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(Guid id)
        {
            try
            {
                var purchase = await _context.Purchases.FindAsync(id);
                if (purchase == null)
                {
                    return NotFound();
                }

                _context.Purchases.Remove(purchase);
                await _context.SaveChangesAsync();

                return Ok(new { status = "success", message = "Deleted Purchase" });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "Purchase Delete Failed" });
            }
        }

        private bool PurchaseExists(Guid id)
        {
            return _context.Purchases.Any(e => e.Purchaseid == id);
        }
    }
}
