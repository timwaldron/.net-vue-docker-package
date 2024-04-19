namespace Web.Api.Models
{
    public class OperationResult
    {
        public string Outcome { get; set; }
        public string Message { get; set; }

        public OperationResult(OperationOutcome result, string message)
        {
            Outcome = result.ToString();
            Message = message;
        }
    }

    public enum OperationOutcome
    {
        Failure,
        Warning,
        Success,
    }
}
