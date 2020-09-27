using System.Collections.Generic;

namespace MyAppAPI.Models
{
    public class OperationResult
    {
        public string Message { get; set; }
        private object Output { get; set; }

        private List<object> Outputs { get; set; }

        public OperationResult(object output = null, 
        string message = "No Report", List<object> outputs = null)
        {
            Message = message;
            Output = output;
            Outputs = outputs;

        }
    }
}