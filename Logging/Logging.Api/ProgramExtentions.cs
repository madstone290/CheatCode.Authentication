using Serilog;
using Serilog.Events;

namespace Logging.Api
{
    public static class ProgramExtentions
    {
        public static void AddSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                // Property를 추가할 수 있다.
                .Enrich.WithProperty("Context", "{SourceContext}")
                .CreateLogger();

            // 기존에 존재하는 로그 제공자를 모두 없애고 세릴로그 로거를 추가한다
            builder.Host.UseSerilog();

            // Serilog.Enrichers.ClientInfo 패키지에서 필요하다.
            builder.Services.AddHttpContextAccessor();
        }
    }
}
