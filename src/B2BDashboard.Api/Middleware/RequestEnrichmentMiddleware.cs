using B2BDashboard.Api.Extensions;
using Serilog.Context;

namespace B2BDashboard.Api.Middleware;

public class RequestEnrichmentMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var companyId = context.User.GetCompanyId();
            using (LogContext.PushProperty("CompanyId", companyId))
            {
                await next(context);
                return;
            }
        }

        await next(context);
    }
}