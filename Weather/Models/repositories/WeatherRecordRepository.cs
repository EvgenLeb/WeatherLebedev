using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Weather.Models.repositories
{
    public interface IWeatherRecordRepository
    {
        IEnumerable<WeatherRecord> GetAll();
        WeatherRecord GetById(int id);
        void Add(WeatherRecord weatherRecord);
        void Update(WeatherRecord weatherRecord);
        void Delete(int id);
        IEnumerable<WeatherRecord> FindByCity(string city);
    }

    public class WeatherRecordRepository : IWeatherRecordRepository
    {
        private readonly MyDbContext _context;

        public WeatherRecordRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WeatherRecord> GetAll()
        {
            return _context.WeatherRecords.ToList();
        }

        public WeatherRecord GetById(int id)
        {
            return _context.WeatherRecords.Find(id);
        }

        public void Add(WeatherRecord weatherRecord)
        {
            _context.WeatherRecords.Add(weatherRecord);
            _context.SaveChanges();
        }

        public void Update(WeatherRecord weatherRecord)
        {
            _context.Entry(weatherRecord).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var weather = _context.WeatherRecords.Find(id);
            if (weather != null)
            {
                _context.WeatherRecords.Remove(weather);
                _context.SaveChanges();
            }
        }

        public IEnumerable<WeatherRecord> FindByCity(string city)
        {
            return _context.WeatherRecords.Where(e => e.City == city).ToList();
        }
    }
}
