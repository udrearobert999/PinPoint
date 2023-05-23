using Domain.Entities;

namespace Domain.Core;

public interface IUnitOfWork
{
    IRepository<User, Guid> Users { get; set; }
    IPinsRepository Pins { get; set; }
    IRepository<PinComment, Guid> PinsComment { get; set; }
    public Task<int> SaveChangesAsync();
    public Task DisposeAsync();

    public int SaveChanges();
    public void Dispose();
}