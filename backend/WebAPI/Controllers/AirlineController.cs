using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace WebAPI.Controllers
{
    [ApiController, Route("airlines")]
    public class AirlineController(MadiContext madiContext) : ControllerBase
    {
        [HttpGet, Route("get")]
        public async Task<IActionResult> GetAirlines()
        {
            return Ok(await madiContext.TestTables.ToListAsync());
        }

        [HttpPost, Route("insert")]
        public async Task<IActionResult> GetAirlines(AirLineDto airLineDto)
        {
            var createdAt = (DateTime.UtcNow);
            createdAt = DateTime.SpecifyKind(createdAt, DateTimeKind.Unspecified);
            var airLine = new AirLine()
            {
                Airline = airLineDto.Airline,
                Name = airLineDto.Flight,
                CreatedAt = createdAt,
                Id = new Random().Next(1000, int.MaxValue)
            };

            madiContext.TestTables.Add(airLine);
            await madiContext.SaveChangesAsync();
            return Ok("Добавилось ОК.");
        }

        public record AirLineDto(string Flight, string Airline);
    }
}
