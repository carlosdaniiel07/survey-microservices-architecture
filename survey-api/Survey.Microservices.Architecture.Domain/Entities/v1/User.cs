using System.Text.Json.Serialization;

namespace Survey.Microservices.Architecture.Domain.Entities.v1
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
