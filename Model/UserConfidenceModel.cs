using System.ComponentModel.DataAnnotations;

namespace appPrevencionRiesgos.Model
{
    public class UserConfidenceModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public string? Status { get; set; }
        UserConfidenceModel(string? email, string status)
        {
            Email = email;
            Status = status;
        }
    }
    public class UserConfidenceExtendedModel
    {
        [Required(ErrorMessage = "Email sender address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? EmailFrom { get; set; }
        [Required(ErrorMessage = "Email receiver address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? EmailTo { get; set; }
        public string PendingStatus = "pending";
        public string SentStatus = "sent";
    }
}
