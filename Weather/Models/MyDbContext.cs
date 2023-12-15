namespace Weather.Models;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<WeatherRecord> WeatherRecords { get; set; }
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }
}