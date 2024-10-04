
namespace Infrastructure.Persistence.Interface
{
  public interface IUnitOfWorkRepository : IDisposable
  {
    #region User
    IUserRepository UserRepository { get; }
    #endregion
    #region Location
    ILocationRepository LocationRepository { get; }
    #endregion
    #region Collection
    ICollectionRepository CollectionRepository { get; }
    #endregion
    #region Material
    IMaterialRepository MaterialRepository { get; }
    #endregion
    #region Answer
    IAnswerRepository AnswerRepository { get; }
    #endregion
    #region Question
    IQuestionRepository QuestionRepository { get; }
    #endregion
    #region UserDeviceLogin
    IUserDeviceLoginRepository UserDeviceLoginRepository { get; }
    #endregion
    #region Departamento
    IDepartamentoRepository DepartamentoRepository { get; }
    #endregion
    #region Municipio
    IMunicipioRepository MunicipioRepository { get; }
    #endregion
    #region Role
    IRoleRepository RoleRepository { get; }
    #endregion
  }
}