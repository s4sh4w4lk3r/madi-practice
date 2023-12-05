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
            return Ok(await airlineService.GetIQueryable().ToListAsync());
        }

        [HttpPost, Route("insert")]
        public async Task<IActionResult> InsertAirlines(AirLineInsertDto airLineDto)
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

        [HttpDelete, Route("delete")]
        public async Task<IActionResult> Delete([FromQuery]int id)
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

        [HttpPatch, Route("update")]
        public async Task<IActionResult> Update([FromBody] AirLineUpdateDto airLineUpdateDto)
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

        [HttpGet, Route("export")]
        public async Task<IActionResult> Export()
        {
            var stream = (await airlineService.GetExcelAsync(airlineService.GetIQueryable().ToList())).Value;
            if (stream is null)
            {
                return BadRequest("Гуляй.");
            }
            stream.Seek(0, SeekOrigin.Begin);
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"База от {DateTime.UtcNow}.xlsx";
            return File(stream, fileType, fileName);
        }

        public record AirLineInsertDto(string Flight, string Airline);
        public record AirLineUpdateDto(int Id, string Flight, string Airline);
    }
}
