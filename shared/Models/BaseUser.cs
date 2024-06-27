using System.Text.Json.Serialization;

public abstract class UserBase
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRole Role { get; set; }

    public UserBase(string name, string surname, string email, UserRole role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Email = email;
        Role = role;
    }

    public string FullName => $"{Name} {Surname}";
}
