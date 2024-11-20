using Microsoft.AspNetCore.Antiforgery;

namespace ProjetoLogin.Libraries.Middleware
{
    public class ValidateAntiForgeryTokenMiddleware
    {
        //ValidateAntiForgeryTokenMiddleware

        private RequestDelegate _next;
        private IAntiforgery _antiforgery;

        public ValidateAntiForgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }
        public async Task Invoke(HttpContext context)
        {
            if (HttpMethods.IsPost(context.Request.Method))
            {
                await _antiforgery.ValidateRequestAsync(context);
            }

            await _next(context);
        }

    }
}
