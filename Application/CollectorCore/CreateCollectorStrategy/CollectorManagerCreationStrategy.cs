using Application.CollectorCore.Dto;
using AutoMapper;
using Domain.Collector;
using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;

namespace Application.CollectorCore.CreateCollectorStrategy;

public class CollectorManagerCreationStrategy : IRoleCreationStrategy
{
  public async Task CreateEntity(
    Collector collector,
    IUnitOfWork unitOfWork,
    IMapper mapper
  )
  {
    // generando collector con role E (gestionador de recicladores)
    try
    {
      
      int[] idUsers = collector.idUsers
        .Split(',')
        .Select(id => int.TryParse(id, out int validId) ? validId : (int?)null)
        .Where(id => id.HasValue) 
        .Select(id => id.Value)
        .ToArray();
      var messageIdInvalid = "Operación no válida, no se encuentran ids validos de para recicladores";
      if (idUsers.Length == 0) 
      {
        throw new InvalidOperationException(messageIdInvalid);
      }
      var users = await  unitOfWork.Repository.UserRepository.GetAllAsync(x => idUsers.Contains(x.Id));
      if (users.Count() == 0) 
      {
        throw new InvalidOperationException(messageIdInvalid);
      }
      
      var collectors = mapper.Map<IEnumerable<Collector>>(users);
      var collectorsIds = collectors.Select(l => l.id).ToHashSet();
      var missingCollectorIds = idUsers.Where(collectorDb => !collectorsIds.Contains(collectorDb));
      if (missingCollectorIds.Count() > 0)
      {
        throw new InvalidOperationException($"Operación no válida, los siguientes Ids de recicladores no existen: {string.Join(", ", missingCollectorIds)}");
      }
      var user = mapper.Map<User>(collector);
      
      await unitOfWork.Repository.UserRepository.AddAsync(user);
      await unitOfWork.SaveChangesAsync();
    } catch (Exception error)
    { 
      throw new InvalidOperationException(error.Message);
    } 
  }
}