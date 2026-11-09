
namespace PolyRibo.Domain.Entities;


public partial class ArchitectureType : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
