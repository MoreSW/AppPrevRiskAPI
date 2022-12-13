namespace appPrevencionRiesgos.Data.Entities
{
    public interface IUserConfidenceEntity
    {
        public string? EmailFrom { get; set; }
        public IDictionary<string, string>? Data { get; set; }
    }
    public class UserConfidenceSenderEntity : IUserConfidenceEntity
    {
        public string? EmailFrom { get; set; }
        public IDictionary<string, string>? Data { get; set; }
        public UserConfidenceSenderEntity(string? emailFrom, string? emailTo)
        {
            EmailFrom = emailFrom;
            Data = new Dictionary<string, string>()
            {
                {"email", emailTo},
                {"status", "sent"},
            };
        }
    }
    public class UserConfidenceReceiverEntity : IUserConfidenceEntity
    {
        public string? EmailFrom { get; set; }
        public IDictionary<string, string>? Data { get; set; }
        public UserConfidenceReceiverEntity(string? emailFrom, string? emailTo)
        {
            EmailFrom = emailFrom;
            Data = new Dictionary<string, string>()
            {
                {"email", emailTo},
                {"status", "pending"},
            };
        }
    }
}
