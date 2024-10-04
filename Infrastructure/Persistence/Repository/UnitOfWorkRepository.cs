using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Repository
{
  public class UnitOfWorkRepository : IUnitOfWorkRepository
  {
    #region User
    public IUserRepository UserRepository { get; }
    #endregion

    #region Location
    public ILocationRepository LocationRepository { get; }
    #endregion
    
    #region Collection
    public ICollectionRepository CollectionRepository { get; }
    #endregion

    #region Material
    public IMaterialRepository MaterialRepository { get; }
    #endregion

    #region Answer
    public IAnswerRepository AnswerRepository { get; }
    #endregion
    #region Question
    public IQuestionRepository QuestionRepository { get; }
    #endregion
    #region UserDeviceLogin
    public IUserDeviceLoginRepository UserDeviceLoginRepository { get; }
    #endregion
    #region Departamento
    public IDepartamentoRepository DepartamentoRepository { get; }
    #endregion
    #region Municipio
    public IMunicipioRepository MunicipioRepository { get; }
    #endregion
    #region Role
    public IRoleRepository RoleRepository { get; }
    #endregion


    public UnitOfWorkRepository(ReciclalosDbContext context)
    {
      #region User
      UserRepository = new UserRepository(context);
      #endregion
     
      #region Location
      LocationRepository = new LocationRepository(context);
      #endregion
      
      #region Collection
      CollectionRepository = new CollectionRepository(context);
      #endregion

      #region Material
      MaterialRepository = new MaterialRepository(context);
      #endregion
 
      #region Answer
      AnswerRepository = new AnswerRepository(context);
      #endregion

      #region Question
      QuestionRepository = new QuestionRepository(context);
      #endregion
    
      #region UserDeviceLogin
      UserDeviceLoginRepository = new UserDeviceLoginRepository(context);
      #endregion
    
      #region Departamento
      DepartamentoRepository = new DepartamentoRepository(context);
      #endregion
  
      #region Municipio
      MunicipioRepository = new MunicipioRepository(context);
      #endregion
      
      #region Role
      RoleRepository = new RoleRepository(context);
      #endregion
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}