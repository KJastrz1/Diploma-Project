using System.Text.Json.Serialization;

namespace Shared.Models;
public abstract class UserBase
{
       public Guid Id { get; set; }
       public string Name { get; set; }
       public string Surname { get; set; }
       public string Email { get; set; }
       [JsonConverter(typeof(JsonStringEnumConverter))]
       public DateTime CreatedAt { get; set; } = DateTime.Now;
       public UserRole Role { get; set; }
}

