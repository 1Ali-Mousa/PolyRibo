
namespace PolyRibo.Domain.Entities;

public partial class Project : BaseEntity
{
    public int UserId { get; set; }

    public int ArchitectureTypeId { get; set; }

    public int FrameworkTypeId { get; set; }

    public int DatabaseId { get; set; }

    public int LanguageId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual ArchitectureType ArchitectureType { get; set; } = null!;

    public virtual ICollection<DataAccessesOfProject> DataAccessesOfProjects { get; set; } = new List<DataAccessesOfProject>();

    public virtual Database Database { get; set; } = null!;

    public virtual DockerConfiguration? DockerConfiguration { get; set; }

    public virtual FrameworkType FrameworkType { get; set; } = null!;

    public virtual GitConfiguration? GitConfiguration { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual ICollection<PackagesOfProject> PackagesOfProjects { get; set; } = new List<PackagesOfProject>();

    public virtual User User { get; set; } = null!;
}
