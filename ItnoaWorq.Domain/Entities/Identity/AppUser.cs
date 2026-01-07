using ItnoaWorq.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ItnoaWorq.Domain.Entities.Identity;

public class AppUser : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? FullName { get; set; }
    public PublicProfile? PublicProfile { get; set; }
    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
}
