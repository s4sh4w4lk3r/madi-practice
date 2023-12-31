﻿using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
using Repository;

namespace WebAPI.Services
{
    public class AirlineService(MadiContext madiContext)
    {
        public async Task<ServiceResult> InsertAsync(AirLine airLine)
        {
            if (string.IsNullOrWhiteSpace(airLine.Airline) || string.IsNullOrWhiteSpace(airLine.Flight))
            {
                return ServiceResult.Fail("Некорректные данные");
            }
            madiContext.TestTables.Add(airLine);
            await madiContext.SaveChangesAsync();
            return ServiceResult.Ok("Полет добавился в бд.");
        }

        public IQueryable<AirLine> GetIQueryable() => madiContext.TestTables.AsQueryable();

        public async Task<ServiceResult> DeleteAsync(int airlineId)
        {
            var airlineToDel = await madiContext.Set<AirLine>().SingleOrDefaultAsync(e=>e.Id == airlineId);
            if (airlineToDel is null)
            {
                return ServiceResult.Fail("Такой записи нет в бд.");
            }

            madiContext.Set<AirLine>().Remove(airlineToDel);
            await madiContext.SaveChangesAsync();
            return ServiceResult.Ok("Ликвидирован.");
        }

        public async Task<ServiceResult> UpdateAsync(AirLine airLine)
        {
            var trackedAirline = await madiContext.Set<AirLine>().SingleOrDefaultAsync(e => e.Id == airLine.Id);
            if (trackedAirline is null)
            {
                return ServiceResult.Fail("Нет записи в бд.");
            }

            var updatedAt = DateTime.UtcNow;
            updatedAt = DateTime.SpecifyKind(updatedAt, DateTimeKind.Unspecified);

            trackedAirline.Airline = airLine.Airline;
            trackedAirline.Flight = airLine.Flight;
            trackedAirline.UpdatedAt = updatedAt;

            await madiContext.SaveChangesAsync();
            return ServiceResult.Ok("Обновлен.");
        }

        public async Task<ServiceResult<MemoryStream>> GetExcelAsync(IEnumerable<AirLine> airLines)
        {
            var stream = new MemoryStream();
            await stream.SaveAsAsync(airLines);
            return ServiceResult<MemoryStream>.Ok("Поток с экселем записан", stream);
        }

        public async Task<ServiceResult> UpdateManyAsync(IEnumerable<AirLine> airLines)
        {
            foreach (var item in airLines)
            {
                var trackedAirline = await madiContext.Set<AirLine>().SingleOrDefaultAsync(e => e.Id == item.Id);
                if (trackedAirline is null)
                {
                    return ServiceResult.Fail("Нет записи в бд.");
                }

                var updatedAt = DateTime.UtcNow;
                updatedAt = DateTime.SpecifyKind(updatedAt, DateTimeKind.Unspecified);

                trackedAirline.Airline = item.Airline;
                trackedAirline.Flight = item.Flight;
                trackedAirline.UpdatedAt = updatedAt;
            }

            await madiContext.SaveChangesAsync();
            return ServiceResult.Ok("Обновлено.");
        }

        public async Task<ServiceResult> ImportExcelAsync(IEnumerable<AirLine> airLines)
        {
            if (airLines is null || !airLines.Any())
            {
                return ServiceResult.Fail("Получен пустой массив данных.");
            }
            var airLinesList = airLines.Where(e => e.Id != default && !string.IsNullOrWhiteSpace(e.Airline) && !string.IsNullOrWhiteSpace(e.Flight)).ToList();
            await madiContext.Set<AirLine>().ForEachAsync(e =>
            {
                var current = airLinesList.SingleOrDefault(x => x.Id == e.Id);
                if (current is not null)
                {
                    airLinesList.Remove(current);
                }
            });

            await madiContext.Set<AirLine>().AddRangeAsync(airLinesList);
            await madiContext.SaveChangesAsync();
            return ServiceResult.Ok("Все сохранилось норм.");
        }
    }
}
