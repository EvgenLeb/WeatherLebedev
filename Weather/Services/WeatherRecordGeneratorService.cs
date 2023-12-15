using System.Text.Json;
using Weather.Services;
using Weather.Models;
public interface IWeatherRecordGeneratorService
{
    Task StartAsync(CancellationToken cancellationToken);
}
public class WeatherRecordGeneratorService : BackgroundService, IWeatherRecordGeneratorService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly HttpClient _httpClient;

    public WeatherRecordGeneratorService(IServiceScopeFactory scopeFactory, IHttpClientFactory httpClientFactory)
    {
        _scopeFactory = scopeFactory;
        _httpClient = httpClientFactory.CreateClient();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var weatherData = await GetWeatherDataAsync();

            using (var scope = _scopeFactory.CreateScope())
            {
                var weatherRecordService = scope.ServiceProvider.GetRequiredService<IWeatherRecordService>();

                var newWeatherRecord = new WeatherRecord
                {
                    City = "Москва",
                    Temperature = weatherData.Infos.Temp,
                    Date = DateTime.UtcNow
                };

                weatherRecordService.Add(newWeatherRecord);
            }

            await Task.Delay(60000, stoppingToken); // Delay for 1 minute
        }
    }

    private async Task<WeatherApiResponse> GetWeatherDataAsync()
    {
        string apiKey = "f02888ae8f6b5d45095dfd7391c10c6a";
        string moscowWeatherUrl = $"http://api.openweathermap.org/data/2.5/weather?q=Moscow&appid={apiKey}&units=metric";

        var response = await _httpClient.GetAsync(moscowWeatherUrl);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent); // Логирование для отладки

        // Десериализация JSON из строки
        var weatherResponse = JsonSerializer.Deserialize<WeatherApiResponse>(responseContent);

        return weatherResponse ?? new WeatherApiResponse();
    }
}
