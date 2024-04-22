using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

namespace v1.Entities
{
    public class ProjectUserRecord
    {
        public int ProjectId { get; set; }
        public int UserRecordId { get; set; }
        public DateTime CreatedAt  { get; set; }
    }
}
