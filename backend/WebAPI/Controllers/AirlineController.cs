using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController, Route("airlines")]
    public class AirlineController(AirlineService airlineService) : ControllerBase
    {
        [HttpGet, Route("get")]
        public async Task<IActionResult> GetAirlines()
        {
            return Ok(await airlineService.SelectAsync().ToListAsync());
        }

        [HttpPost, Route("insert")]
        public async Task<IActionResult> GetAirlines(AirLineInsertDto airLineDto)
        {
            var createdAt = DateTime.UtcNow;
            createdAt = DateTime.SpecifyKind(createdAt, DateTimeKind.Unspecified);
            var airLine = new AirLine()
            {
                Airline = airLineDto.Airline,
                Name = airLineDto.Flight,
                CreatedAt = createdAt,
                Id = new Random().Next(1000, int.MaxValue)
            };

            var airlineResult = await airlineService.InsertAsync(airLine);

            if (airlineResult.Success is false)
            {
                return BadRequest(airlineResult);
            }
            return Ok("Добавилось ОК.");

        }

        [HttpPost, Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == default)
            {
                return BadRequest("Айди ноль.");
            }

            var serviceResult = await airlineService.DeleteAsync(id);
            if (serviceResult.Success is false)
            {
                return BadRequest(serviceResult);
            } 
            return Ok(serviceResult);
        }

        [HttpPost, Route("update")]
        public async Task<IActionResult> Update(AirLineUpdateDto airLineUpdateDto)
        {
            if (airLineUpdateDto.Id == default || string.IsNullOrWhiteSpace(airLineUpdateDto.Flight) || string.IsNullOrWhiteSpace(airLineUpdateDto.Airline))
            {
                return BadRequest("Валидация не пройдена");
            }

            var airline = new AirLine()
            {
                Airline = airLineUpdateDto.Airline,
                Name = airLineUpdateDto.Flight,
                Id = airLineUpdateDto.Id,
            };

            var result = await airlineService.UpdateAsync(airline);
            if (result.Success is false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        public record AirLineInsertDto(string Flight, string Airline);
        public record AirLineUpdateDto(int Id, string Flight, string Airline);
    }
}
