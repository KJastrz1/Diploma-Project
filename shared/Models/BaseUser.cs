using System.Text.Json.Serialization;

namespace Shared.Models;
public abstract class UserBase
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRole Role { get; set; }


    public string FullName => $"{Name} {Surname}";
}

