

namespace Micromarin.Domain.Interfaces;

public interface IBaseEntity
{
    DateTime CreatedDate { get; set; }
    Guid Id { get; set; }
    DateTime? UpdatedDate { get; set; }
}
