#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Car.Entities;
using Car.Helpers;

namespace Car.Models
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly carContext _context;

        public CarController(carContext context)
        {
            _context = context;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                var car = await _context.Cars.Include(m => m.User).ToListAsync();
                return Ok(new { status = "success", data = car, message = "Get All Car Successful" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "Get All Car Failed" });
            }
        }

        // GET: api/Car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarById(Guid id)
        {
            try
            {
                var car = await _context.Cars.FindAsync(id);
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(new { status = "success", data = car, message = "Get Car By Id Successful" });
            }
            catch (System.Exception ex)
            {
                return NotFound(new { status = "failed", serverMessage = ex.Message, message = "Car Not Found" });
            }
        }

        // PUT: api/Car/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(Guid id, Car car)
        {
            if (id != car.Carid)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CarExists(id))
                {
                    return NotFound(new { status = "failed", message = "No Car Found" });
                }
                else
                {
                    Console.WriteLine(ex);
                    return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "Update User By Id Failed" });
                }
            }

            return Ok(new { status = "success", message = "Car Updated Successfully" });
        }

        // POST: api/Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> RegisterNewCar(Car car)
        {
            try
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCarById", new { id = car.Carid }, new { status = "success", data = car, message = "Register Car Successful" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "Register Car failed" });
            }
        }

        // DELETE: api/Car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            try
            {
                var car = await _context.Cars.FindAsync(id);
                if (car == null)
                {
                    return NotFound();
                }

                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();

                return Ok(new { status = "success", message = "Deleted User" });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { status = "failed", serverMessage = ex.Message, message = "Car Delete Failed" });
            }
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Carid == id);
        }
    }
}
