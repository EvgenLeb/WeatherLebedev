using System.Text.Json.Serialization;

public class WeatherApiResponse
{
    [JsonPropertyName("main")]
    public Info Infos { get; set; }

    public class Info
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
    }

}

