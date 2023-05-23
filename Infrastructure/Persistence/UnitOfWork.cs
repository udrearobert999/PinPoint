using System.Linq.Expressions;
using Domain.Core;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
        Users = new Repository<User, Guid>(dbContext);
        Pins = new PinsRepository(dbContext);
        PinsComment = new Repository<PinComment, Guid>(dbContext);
    }

    public IRepository<User, Guid> Users { get; set; }
    public IPinsRepository Pins { get; set; }
    public IRepository<PinComment, Guid> PinsComment { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}