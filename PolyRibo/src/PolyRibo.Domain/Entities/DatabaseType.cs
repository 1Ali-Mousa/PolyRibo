
namespace PolyRibo.Domain.Entities;

public partial class DatabaseType : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Database> Databases { get; set; } = new List<Database>();
}
