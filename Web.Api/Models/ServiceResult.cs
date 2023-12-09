using System.Collections.Generic;

namespace Web.Api.Models
{
    public class ServiceResult<T> where T : class
    {
        public T? Result { get; set; }
        public string Status { get; set; }
        public List<ServiceResultMessage> Messages { get; set; }

        public ServiceResult()
        {
            Result = null;
            Status = ServiceResultStatus.NA.ToString();
            Messages = new List<ServiceResultMessage>();
        }

        public ServiceResult(T result, ServiceResultStatus status)
        {
            Result = result;
            Status = status.ToString();
            Messages = new List<ServiceResultMessage>();
        }

        public ServiceResult(T result, ServiceResultStatus status, string message)
        {
            Result = result;
            Status = status.ToString();
            Messages = new List<ServiceResultMessage>
            {
                new ServiceResultMessage { Message = message }
            };
        }

        public void AddMessage(string message)
        {
            Messages.Add(new ServiceResultMessage { Message = message });
        }

        // This will work for now. I cba writing an enum attribute to go help going between enum <=> string right now
        public void SetStatus(ServiceResultStatus status)
        {
            Status = status.ToString();
        }
    }

    public class ServiceResultMessage
    {
        // TODO: Potentially more information about the message
        // public string Field { get; set; }
        public string Message { get; set; }
    }

    public enum ServiceResultStatus
    {
        NA,
        Success,
        Warning,
        Failure,
    }
}
