using System.Net;
using System.Text.Json;

namespace LeaseManager.WebAPI.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse();

            switch (exception)
            {
                case InvalidOperationException ex:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Opération invalide",
                        Details = ex.Message
                    };
                    break;

                case ArgumentNullException ex:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Un argument requis est manquant",
                        Details = ex.Message
                    };
                    break;

                case KeyNotFoundException ex:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response = new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Ressource non trouvée",
                        Details = ex.Message
                    };
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response = new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Erreur serveur interne",
                        Details = "Une erreur inattendue s'est produite"
                    };
                    break;
            }

            return context.Response.WriteAsJsonAsync(response);
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}