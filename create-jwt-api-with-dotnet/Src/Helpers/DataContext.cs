namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    //IConfiguration ถูกใช้เพื่ออ่านค่าการตั้งค่าจากไฟล์เช่น appsettings.json
    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // มีการใช้งาน options.UseMySql() เพื่อเชื่อมต่อกับฐานข้อมูล MySQL โดยใช้ connection string จาก DefaultConnection
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
    }
}