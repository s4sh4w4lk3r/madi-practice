using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
using Repository;
using System;
using System.IO;
using WebAPI.Services;
using static WebAPI.Controllers.AirlineController;

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
                Flight = airLineDto.Flight,
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
        public async Task<IActionResult> Delete([FromQuery] int id)
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
                Flight = airLineUpdateDto.Flight,
                Id = airLineUpdateDto.Id,
            };

            var result = await airlineService.UpdateAsync(airline);
            if (result.Success is false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch, Route("updatemany")]
        public async Task<IActionResult> UpdateMany([FromBody] AirLineUpdateManyDto airLineUpdateManyDto)
        {
            var airLineUpdateDtos = airLineUpdateManyDto.AirLineUpdateDtos;
            foreach (var airLineUpdateDto in airLineUpdateDtos)
            {
                if (airLineUpdateDto.Id == default || string.IsNullOrWhiteSpace(airLineUpdateDto.Flight) || string.IsNullOrWhiteSpace(airLineUpdateDto.Airline))
                {
                    return BadRequest($"Элемент с ID {airLineUpdateDto.Id} не прошел валидацию.");
                }
            }

            var airlinesToSave = airLineUpdateDtos.Select(e => new AirLine()
            {
                Airline = e.Airline,
                Flight = e.Flight,
                Id = e.Id,
            });

            var result = await airlineService.UpdateManyAsync(airlinesToSave);
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

        [HttpPost, Route("import")]
        public async Task<IActionResult> Import(IFormFile formFile)
        {
            using var stream = formFile.OpenReadStream();
            var airlnes = stream.Query<AirLine>();
            var result = await airlineService.ImportExcelAsync(airlnes);

            if (result.Success is false)
            {
                return BadRequest(result);
            }
            return Ok();
        }

        public record AirLineInsertDto(string Flight, string Airline);
        public record AirLineUpdateDto(int Id, string Flight, string Airline);
        public record AirLineUpdateManyDto(IEnumerable<AirLineUpdateDto> AirLineUpdateDtos);
        public record AirLineInsertManyDto(IEnumerable<AirLineInsertDto> AirLineInsertDtos);
    }
}
