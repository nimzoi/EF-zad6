using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EfExample.Context;
using EfExample.Models;
using System.Threading.Tasks;
using System.Linq;
using TripEF.DTOs;
using TripEF.Models;

namespace EfExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly TripContext _context;

        public TripController(TripContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetTrips()
        {
            var trips = await _context.Trips
                .OrderByDescending(t => t.StartDate)
                .Select(t => new
                {
                    t.TripId,
                    t.Name,
                    t.StartDate,
                    t.EndDate,
                    Clients = t.ClientTrips.Select(ct => new 
                    {
                        ct.Client.FirstName,
                        ct.Client.LastName,
                        ct.PaymentDate
                    })
                })
                .ToListAsync();

            return Ok(trips);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] ClientTripDTO clientTripDTO)
        {
            var trip = await _context.Trips.FindAsync(idTrip);
            if (trip == null)
            {
                return NotFound("Trip not found.");
            }

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == clientTripDTO.Pesel);
            if (client == null)
            {
                client = new Client
                {
                    Pesel = clientTripDTO.Pesel,
                    FirstName = clientTripDTO.FirstName,
                    LastName = clientTripDTO.LastName
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
            }

            if (await _context.ClientTrips.AnyAsync(ct => ct.ClientId == client.ClientId && ct.TripId == idTrip))
            {
                return BadRequest("Client is already assigned to this trip.");
            }

            var clientTrip = new ClientTrip
            {
                ClientId = client.ClientId,
                TripId = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientTripDTO.PaymentDate
            };

            _context.ClientTrips.Add(clientTrip);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
