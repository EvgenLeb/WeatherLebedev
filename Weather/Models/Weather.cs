namespace Weather.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class WeatherRecord
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public double Temperature { get; set; }

    [Required]
    public DateTime Date { get; set; }
}
