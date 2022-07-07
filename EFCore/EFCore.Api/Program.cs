using EFCore.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\_Home\Sources\VisualStudio\managed\CheatCode\EFCore.Core\UserDb.mdf;Integrated Security=True";

// 1. 시작프로젝트 선택
// 2. PackageManageConsole에서 DefaultProject로 DbContext가 속한 프로젝트 선택
//      - 마이그레이션 어셈블리는 기본값으로 DbContext가 속한 어셈블리
//      - 다른 어셈블리에 마이그레이션을 저장하려면  options.UseSqlServer(connection, b => b.MigrationsAssembly("EFCore.Api")) 사용할 것.
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {

    });
});

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
