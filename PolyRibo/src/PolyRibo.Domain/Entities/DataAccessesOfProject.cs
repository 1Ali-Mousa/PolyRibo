
namespace PolyRibo.Domain.Entities;
public partial class DataAccessesOfProject :BaseEntity
{
    public int ProjectId { get; set; }

    public int DataAccessTechnologyId { get; set; }

    public virtual DataAccessTechnology DataAccessTechnology { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
