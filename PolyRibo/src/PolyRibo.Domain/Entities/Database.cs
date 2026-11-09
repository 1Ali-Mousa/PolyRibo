
namespace PolyRibo.Domain.Entities;
public partial class Database : BaseEntity
{
    public int DatabaseTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string BackUpFileName { get; set; } = null!;

    public DateTime UploadedAt { get; set; }

    public virtual ICollection<DatabaseTable> DatabaseTables { get; set; } = new List<DatabaseTable>();

    public virtual DatabaseType DatabaseType { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
