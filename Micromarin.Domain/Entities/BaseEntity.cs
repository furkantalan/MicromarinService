using Micromarin.Domain.Interfaces;

namespace Micromarin.Domain.Entities;

public abstract class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    virtual public DateTime? UpdatedDate { get; set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}