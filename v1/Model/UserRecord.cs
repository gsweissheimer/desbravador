using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

namespace v1.Entities
{
    public class UserRecord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
