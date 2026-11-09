
namespace PolyRibo.Domain.Entities;

public partial class FrameworkType : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Version { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
