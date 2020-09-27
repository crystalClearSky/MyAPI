namespace MyAppAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsApproved { get; set; }

        public ErrorMessage IsValid(object toValidate){
            IsApproved = false;
            ErrorMessage errorMessage = new ErrorMessage("", false);
            var objectTypes = toValidate.GetType();
            if(toValidate is string)
            {
                var result = (string)toValidate;
            }
            if(this.Message != null) {
                // Check if string is valid.
            }
            // Will approved once all is validated
            return errorMessage;
        }
    }

}