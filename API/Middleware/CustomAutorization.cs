namespace API.Middleware;

public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.ContentType = "application/json";
            var response = new { message = "Usuario no autorizado para esta acción" };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
