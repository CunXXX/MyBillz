using Common;
using MyBillzService.Logging;
using MyBillzService.Repositories;
using MyBillzService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDBService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("§ä¤£¨ì³s½u¦r¦ê");
    return new MSSQLService(connectionString);
});

// µù¥URepository
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

// µù¥UService
builder.Services.AddScoped<IMemberService, MemberService>();

// µù¥ULogging
builder.Services.AddScoped<ILoggingService, LoggingService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
