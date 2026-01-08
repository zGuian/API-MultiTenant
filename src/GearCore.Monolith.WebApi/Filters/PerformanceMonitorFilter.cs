using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace GearCore.Monolith.WebApi.Filters
{
    public class PerformanceMonitorFilter(ILogger<PerformanceMonitorFilter> logger) : IResultFilter
    {
        public readonly ILogger<PerformanceMonitorFilter> _logger = logger;
        private Stopwatch _stopwatch = new();

        public void OnResultExecuted(ResultExecutedContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"Iniciado ação: {actionName}");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _stopwatch.Stop();
            var actionName = context.ActionDescriptor.DisplayName;
            var elapsedTime = _stopwatch.ElapsedMilliseconds;
            var statusCode = context.HttpContext.Response.StatusCode;
            _logger.LogInformation($"Action: '{actionName}', executado em {elapsedTime} ms com status code {statusCode}");
        }
    }
}
