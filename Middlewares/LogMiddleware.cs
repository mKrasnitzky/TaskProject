using System.Diagnostics;

namespace TaskProject.Middlewares;

public class LogMiddleware
{
    private RequestDelegate next;
    private readonly string logger;

    public LogMiddleware(RequestDelegate next, string logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext c)
    {
        var sw = new Stopwatch();
        sw.Start();
        await next(c);
        WriteLogToFile($"{c.Request.Path}.{c.Request.Method} took {sw.ElapsedMilliseconds}ms."
            + $" User: {c.User?.FindFirst("userId")?.Value ?? "unknown"}");  
    }
    private void WriteLogToFile(string logMessage)
        {
            using (StreamWriter sw = File.AppendText(logger))
            {
                sw.WriteLine(logMessage);
            }
        } 
}


public static partial class MiddleExtensions
{
    public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder,string logFilePath)
    {
        return builder.UseMiddleware<LogMiddleware>(logFilePath);
    }
}