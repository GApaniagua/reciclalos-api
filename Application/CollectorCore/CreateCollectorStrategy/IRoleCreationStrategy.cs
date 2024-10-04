using Application.CollectorCore.Dto;
using AutoMapper;
using Domain.Collector;
using Infrastructure.Persistence.Interface;

public interface IRoleCreationStrategy
{
  public  Task CreateEntity(
    Collector collector,
    IUnitOfWork  unitOfWork,
    IMapper mapper
  );
}