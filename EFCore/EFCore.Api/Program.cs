using EFCore.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\_Home\Sources\VisualStudio\managed\CheatCode\EFCore\EFCore.Core\UserDb.mdf;Integrated Security=True";

// 1. ����������Ʈ ����
// 2. PackageManageConsole���� DefaultProject�� DbContext�� ���� ������Ʈ ����
//      - ���̱׷��̼� ������� �⺻������ DbContext�� ���� �����
//      - �ٸ� ������� ���̱׷��̼��� �����Ϸ���  options.UseSqlServer(connection, b => b.MigrationsAssembly("EFCore.Api")) ����� ��.
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
