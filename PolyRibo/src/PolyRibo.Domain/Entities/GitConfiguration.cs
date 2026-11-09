
namespace PolyRibo.Domain.Entities;

public partial class GitConfiguration : BaseEntity
{
    public int ProjectId { get; set; }

    public bool IsEnabled { get; set; }

    public string? RemoteUrl { get; set; }

    public string? DefaultBranch { get; set; }

    public virtual Project Project { get; set; } = null!;
}
