
using Application.Common;
using AutoMapper;
using Domain.CollectionCenter;
using Domain.Collector;
using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Models;


namespace Application.CollectorCore.CreateCollectorStrategy;
public class CollectorCreationStrategy : IRoleCreationStrategy
{
  public async Task CreateEntity(Collector collector, IUnitOfWork unitOfWork, IMapper mapper)
  {
    // generando collector con role U (reciclador)
    try
        { 
            int[] idLocations = collector.IdLocations
              .Split(',')
              .Select(id => int.TryParse(id, out int validId) ? validId : (int?)null)  // Devuelve el valor válido o null
              .Where(id => id.HasValue)  // Filtra solo los valores válidos
              .Select(id => id.Value)    // Convierte los valores nullable a enteros
              .ToArray();
            var messageIdInvalid = "Operación no válida, no se encuentran ids validos de para centros de acopio";
            if (idLocations.Length == 0)
            {
                throw new InvalidOperationException(messageIdInvalid);
            }
            var locations = await unitOfWork.Repository.LocationRepository.GetAllAsync(x => idLocations.Contains(x.Id));
            if (locations.Count() == 0)
            {
                throw new InvalidOperationException(messageIdInvalid);
            }
            var collectionCenters = mapper.Map<IEnumerable<CollectionCenter>>(locations);
            // Paso 2: Obtener los IDs de los locations
            var locationIds = locations.Select(l => l.Id).ToHashSet();

            // Paso 3: Encontrar los IDs que están en collector pero no en locations
            var missingCCIds = idLocations.Where(id => !locationIds.Contains((short)id)).ToList();
            if (missingCCIds.Count() > 0)
            {
                throw new InvalidOperationException($"Operación no válida, los siguientes Ids de centros de acopio no existen: {string.Join(", ", missingCCIds)}");
            }

            var user = mapper.Map<User>(collector);
            string hashedPassword = PasswordEncrypt(collector);
            user.Password = hashedPassword;
            await unitOfWork.Repository.UserRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception error)
    { 
      throw new InvalidOperationException(error.Message);
    } 
  }

    private static string PasswordEncrypt(Collector collector)
    {
      var passwordEncryptor = new PasswordEncryptor();
      string hashedPassword =  passwordEncryptor.EncryptPassword(collector.password);
      Console.WriteLine($"Hash: {hashedPassword}");
      return hashedPassword;
    }
}