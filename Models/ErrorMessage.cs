namespace MyAppAPI.Models
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public bool HasPassed { get; set; }
        public ErrorMessage(string message, bool hasPassed)
        {
            this.Message = message;
            this.HasPassed = hasPassed;
        }
    }
}