using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

// 1. การสร้าง WebApplication
var builder = WebApplication.CreateBuilder(args); // ส่วนนี้ทำการสร้าง WebApplication โดยรับค่า args จากการเริ่มต้นโปรแกรม

// 2. การตั้งค่า Service Container
{
    var services = builder.Services;
    var env = builder.Environment;

    // 3. การเพิ่ม DbContext
    services.AddDbContext<DataContext>();

    // 4. Register CORS 
    services.AddCors();

    //5. การตั้งค่า Controllers
    // เพิ่ม Controllers สำหรับการจัดการ HTTP requests และตั้งค่า JSON serialization
    services.AddControllers().AddJsonOptions(x =>
        {
            // Serialize enums as strings: ทำให้ enum ถูกแปลงเป็น string ใน JSON responses
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            // Ignore null values: ไม่แสดงค่าที่เป็น null ใน JSON
            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            // Pretty print: ทำให้ JSON readable มากขึ้น (มีการจัดรูปแบบ)
            x.JsonSerializerOptions.WriteIndented = true;

            // Reference handler: ใช้เพื่อจัดการกับ object cycles ที่อาจเกิดขึ้น
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        }
        );

    // 6. การตั้งค่าการเข้าถึง Configuration
    // ทำการตั้งค่าคอนฟิกของแอปพลิเคชันให้เข้าถึงข้อมูลจากไฟล์ config
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // 7. การตั้งค่า Dependency Injection
    // ลงทะเบียนบริการที่ต้องใช้ในโปรเจกต์ เช่น JWT Utilities และ User Service
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();
}
// 8. การตั้งค่า Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 9. การสร้าง WebApplication สร้างแอปพลิเคชันจากการตั้งค่าที่ทำไว้
var app = builder.Build();

// 10. การ Seed ข้อมูลใน Database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

    //ลบฐานข้อมูลก่อนทุกครั้งเมื่อเริ่มโปรแกรม
    context.Database.EnsureDeleted();

    // Make a DB Migration
    context.Database.Migrate();

    // Add hardcoded test and admin user to db on startup
    // Add a User - Test  
    var testUser = new User
    {
        FirstName = "Test",
        LastName = "Test",
        Username = "test",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("test")
    };
    context.Users.Add(testUser);

    // Save the Users to the DB
    context.SaveChanges();

}

// 11. การตั้งค่า Middleware
// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // ใช้ Middleware สำหรับจัดการข้อผิดพลาดและ JWT Authentication
    app.UseMiddleware<JwtMiddleware>();
    //สร้างเส้นทางสำหรับ Controller
    app.MapControllers();
}

// 12. การตั้งค่าสำหรับสภาพแวดล้อมการพัฒนา
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//13. การใช้ HTTPS Redirection
app.UseHttpsRedirection();

app.Run();
