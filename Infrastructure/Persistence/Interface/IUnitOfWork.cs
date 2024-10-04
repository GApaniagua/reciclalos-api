using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence.Interface
{
 public interface IUnitOfWork
  {
    void DetectChanges();
    void SaveChanges();
    Task SaveChangesAsync();
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync();
    void CommitTransaction();
    void RollbackTransaction();
    IUnitOfWorkRepository Repository { get; }
  } 
  
}