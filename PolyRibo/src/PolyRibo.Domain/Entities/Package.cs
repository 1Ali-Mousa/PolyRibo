
namespace PolyRibo.Domain.Entities;


public partial class Package : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Version { get; set; } = null!;

    public virtual ICollection<PackagesOfProject> PackagesOfProjects { get; set; } = new List<PackagesOfProject>();
}
