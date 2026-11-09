
namespace PolyRibo.Domain.Entities;

public partial class Country : BaseEntity
{

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
