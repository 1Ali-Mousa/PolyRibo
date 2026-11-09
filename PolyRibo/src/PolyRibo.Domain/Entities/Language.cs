
namespace PolyRibo.Domain.Entities;

public partial class Language : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
