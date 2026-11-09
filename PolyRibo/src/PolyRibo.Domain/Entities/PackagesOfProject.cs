
namespace PolyRibo.Domain.Entities;


public partial class PackagesOfProject : BaseEntity
{
    public int ProjectId { get; set; }

    public int PackageId { get; set; }

    public virtual Package Package { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
