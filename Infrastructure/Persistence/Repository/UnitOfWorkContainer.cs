using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence.Repository;

public class UnitOfWorkContainer : IUnitOfWork
{
  private readonly ReciclalosDbContext _context;
  public IUnitOfWorkRepository Repository { get; }

  public UnitOfWorkContainer(
    ReciclalosDbContext context
  )
  {
    _context = context;
    Repository = new UnitOfWorkRepository(_context);
  }

  #region Detect Changes
  public void DetectChanges()
  {
    _context.ChangeTracker.DetectChanges();
  }
  #endregion

  #region Save Changes
  public void SaveChanges()
  {
    _context.SaveChanges();
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
  #endregion

  #region Transactions
  public IDbContextTransaction BeginTransaction()
  {
    return _context.Database.BeginTransaction();
  }

  public async Task<IDbContextTransaction> BeginTransactionAsync()
  {
    return await _context.Database.BeginTransactionAsync();
  }

  public void CommitTransaction()
  {
    _context.Database.CommitTransaction();
  }

  public void RollbackTransaction()
  {
    _context.Database.RollbackTransaction();
  }
  #endregion
}
