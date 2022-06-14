using Logging.Api;
using Logging.Api.Filters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<SerilogLoggingActionFilter>();
});

//builder.Services.AddHttpLogging(logging =>
//{
//});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(options =>
{
    //options.MessageTemplate = "Client {Client}";

    //options.EnrichDiagnosticContext = (d, c) =>
    //{
    //    d.Set("Client", c.Connection.RemoteIpAddress.MapToIPv4().ToString());
    //};
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


Log.CloseAndFlush();