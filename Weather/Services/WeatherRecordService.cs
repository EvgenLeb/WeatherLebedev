using Weather.Models.repositories;
using Weather.Models;

namespace Weather.Services;

public interface IWeatherRecordService
{
    IEnumerable<WeatherRecord> SearchByCity(string coordinates);
    IEnumerable<WeatherRecord> GetAll();
    void Add(WeatherRecord weather);
}
public class WeatherRecordService : IWeatherRecordService
{
    private readonly IWeatherRecordRepository _repository;

    public WeatherRecordService(IWeatherRecordRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<WeatherRecord> SearchByCity(string coordinates)
    {
        return _repository.FindByCity(coordinates);
    }

    public IEnumerable<WeatherRecord> GetAll()
    {
        return _repository.GetAll();
    }
    public void Add(WeatherRecord weather)
    {
        _repository.Add(weather);
    }
 
}

