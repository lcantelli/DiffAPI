namespace DiffAPI.Models
{
    public class Response
    {
        public bool Success { get; }

        public string Message { get; }

        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}