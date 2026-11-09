
namespace PolyRibo.Domain.Entities;

public partial class DatabaseColumn : BaseEntity
{
    public int DatabaseTableId { get; set; }

    public string Name { get; set; } = null!;

    public string DataType { get; set; } = null!;

    public bool IsNullable { get; set; }

    public bool IsPrimaryKey { get; set; }

    public bool IsIdentity { get; set; }

    public int? FkcolumnId { get; set; }

    public virtual DatabaseTable DatabaseTable { get; set; } = null!;

    public virtual DatabaseColumn? Fkcolumn { get; set; }

    public virtual ICollection<DatabaseColumn> InverseFkcolumn { get; set; } = new List<DatabaseColumn>();
}
