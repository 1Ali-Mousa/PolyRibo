
namespace PolyRibo.Domain.Entities;
public partial class User : BaseEntity
{
    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHashed { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
