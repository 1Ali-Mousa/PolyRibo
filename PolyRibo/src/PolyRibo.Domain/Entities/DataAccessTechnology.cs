
namespace PolyRibo.Domain.Entities;

public partial class DataAccessTechnology : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<DataAccessesOfProject> DataAccessesOfProjects { get; set; } = new List<DataAccessesOfProject>();
}
