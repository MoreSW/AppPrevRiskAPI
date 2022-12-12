namespace appPrevencionRiesgos.Data.Entities
{
    public class UserConfidenceEntity
    {
        public IDictionary<string, string> data { get; set; }
        public UserConfidenceEntity(string? email, string? status)
        {
            data = new Dictionary<string, string>() { 
                { "Email" , email }, 
                { "Status" , status },
            };
        }
    }
}
