
namespace PolyRibo.Domain.Entities;


public partial class DatabaseTable : BaseEntity
{
    public int DatabaseId { get; set; }

    public string Name { get; set; } = null!;

    public string SchemaName { get; set; } = null!;

    public virtual Database Database { get; set; } = null!;

    public virtual ICollection<DatabaseColumn> DatabaseColumns { get; set; } = new List<DatabaseColumn>();
}
