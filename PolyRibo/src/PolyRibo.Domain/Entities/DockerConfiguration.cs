
namespace PolyRibo.Domain.Entities;

public partial class DockerConfiguration : BaseEntity
{
    public int ProjectId { get; set; }

    public bool IsEnabled { get; set; }

    public bool DockerFileEnabled { get; set; }

    public bool ComposeEnabled { get; set; }

    public virtual Project Project { get; set; } = null!;
}
