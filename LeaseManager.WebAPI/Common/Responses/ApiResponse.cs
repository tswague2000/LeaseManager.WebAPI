namespace LeaseManager.WebAPI.Common.Responses
{
    /// <summary>
    /// Réponse standardisée API
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public ApiResponse(bool success, string message, T? data = default, IEnumerable<string>? errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }

        /// <summary>
        /// Créer une réponse de succès
        /// </summary>
        public static ApiResponse<T> SuccessResponse(T data, string message = "Opération réussie")
        {
            return new ApiResponse<T>(true, message, data);
        }

        /// <summary>
        /// Créer une réponse d'erreur
        /// </summary>
        public static ApiResponse<T> ErrorResponse(string message, IEnumerable<string>? errors = null)
        {
            return new ApiResponse<T>(false, message, errors: errors);
        }
    }

    /// <summary>
    /// Réponse sans données
    /// </summary>
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string>? Errors { get; set; }

        public ApiResponse(bool success, string message, IEnumerable<string>? errors = null)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }

        public static ApiResponse SuccessResponse(string message = "Opération réussie")
        {
            return new ApiResponse(true, message);
        }

        public static ApiResponse ErrorResponse(string message, IEnumerable<string>? errors = null)
        {
            return new ApiResponse(false, message, errors);
        }
    }
}