using Infrastructure.Persistence.Models;
using Domain.Collector;
using Application.CollectorCore.Dto;
using Domain.CollectionCenter;
using Application.CollectionCenterCore.Dto;
using Domain.Catalog;
using Application.CatalogCore.Dto;
using AutoMapper;
using Domain.Material;
using Application.GameCore;

namespace Application.ProfileMapping;

public class MapperProfile : Profile
{
  public MapperProfile()
  {
    #region Collector
    CreateMap<User, Collector>()
      .ForMember(d => d.id, o => o.MapFrom(m => m.Id))
      .ForMember(d => d.name, o => o.MapFrom(m => m.Name))
      .ForMember(d => d.username, o => o.MapFrom(m => m.Username));

    CreateMap<Collector, User>().ReverseMap();

    CreateMap<Collector, CollectorViewDto>()
    .ForMember(d => d.collectorCentersIds, o => o.MapFrom(m => m.IdLocations))
    .ForMember(d => d.collectorTypeId, o => o.MapFrom(m => m.type))
    .ForMember(d => d.type, o => o.Ignore())
    .ForMember(d => d.imageUrl, o => o.MapFrom(m => m.logo));

    CreateMap<CreateCollector, Collector>()
      .ForMember(d => d.IdLocations, o => o.MapFrom(m => m.CollectorCentersIds))
      .ForMember(d => d.idUsers, o => o.MapFrom(m => m.CollectorsIds))
      .ForMember(d => d.type, o => o.MapFrom(m => m.CollectorTypeId))
      .ForMember(d => d.logo, o => o.MapFrom(m => m.Image));

    #endregion

    #region CollectionCenter
    CreateMap<Location, CollectionCenter>()
      .ForMember(d => d.Station, o => o.MapFrom(m => m.Estacion))
      .ForMember(d => d.Department, o => o.MapFrom(m => m.Departamento))
      .ForMember(d => d.Municipality, o => o.MapFrom(m => m.Municipio))
      .ForMember(d => d.Name, o => o.MapFrom(m => m.Name))
      .ForMember(d => d.Latitude, o => o.MapFrom(m =>
        m != null && m.Latlng != null
        ? m.Latlng.Split(',', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()
        : string.Empty))
      .ForMember(d => d.Longitude, o => o.MapFrom(m =>
        m != null && m.Latlng != null
        ? m.Latlng.Split(',', StringSplitOptions.RemoveEmptyEntries).Skip(1).FirstOrDefault()
        : string.Empty));

    CreateMap<CollectionCenter, CollectionCenterDto>()
  .ForMember(d => d.address, o => o.MapFrom(m => m.Address))
  .ForMember(d => d.name, o => o.MapFrom(m => m.Name))
  .ForMember(d => d.department, o => o.MapFrom(m => m.Department))
  .ForMember(d => d.municipality, o => o.MapFrom(m => m.Municipality))
  .ForMember(d => d.latitude, o => o.MapFrom(m => m.Latitude))
  .ForMember(d => d.longitude, o => o.MapFrom(m => m.Longitude))
  .ForMember(d => d.materials, o => o.Ignore());

    #endregion


    #region Catalogs
    CreateMap<Collection, Catalog>()
      .ForMember(d => d.id, o => o.MapFrom(m => m.Id))
      .ForMember(d => d.latas, o => o.MapFrom(m => m.Latas))
      .ForMember(d => d.papel, o => o.MapFrom(m => m.Papel));

    CreateMap<Catalog, MaterialType>()
      .ForMember(d => d.id, o => o.MapFrom(m => m.id))
      .ForMember(d => d.name, o => o.MapFrom(m => m.latas));
    // .ForMember(d => d.imageUrl, o => o.MapFrom(m => m.??)) de donde vienen
    // .ForMember(d => d.color, o => o.MapFrom(m => m.??))  de donde vienen

    CreateMap<Catalog, MaterialRecyclableType>()
      .ForMember(d => d.id, o => o.MapFrom(m => m.id))
      .ForMember(d => d.name, o => o.MapFrom(m => m.latas));

    CreateMap<CollectionCenterDto, Department>()
      .ForMember(d => d.id, o => o.MapFrom(m => m.id))
      .ForMember(d => d.name, o => o.MapFrom(m => m.department));

    CreateMap<CollectionCenterDto, Municipality>()
      .ForMember(d => d.id, o => o.MapFrom(m => m.id))
      .ForMember(d => d.name, o => o.MapFrom(m => m.municipality));
    #endregion

    #region Material
    CreateMap<Material, MaterialTypes>().ReverseMap();
    #endregion

    #region Departamento
    CreateMap<Departamento, Department>()
      .ForMember(d => d.name, o => o.MapFrom(m => m.Nombre));
    #endregion

    #region Municipio
    CreateMap<Municipio, Municipality>()
      .ForMember(d => d.name, o => o.MapFrom(m => m.Nombre))
      .ForMember(d => d.departmentId, o => o.MapFrom(m => m.DepartamentoId));
    #endregion

    #region Game
    CreateMap<Question, QuestionDTO>().ReverseMap();
    CreateMap<Answer, AnswerDto>().ReverseMap();
    #endregion

    #region Role
    CreateMap<Role, CollectorTypeDto>().ReverseMap();
    #endregion

    #region Report
    CreateMap<Collection, ReportDto>()
    .ForMember(d => d.TetraPak, o => o.MapFrom(m => m.Tetrapak))
    .ForMember(d => d.IdCentroAcopio, o => o.MapFrom(m => m.IdLocation))
    .ForMember(d => d.EnvasesVidrio, o => o.MapFrom(m => m.Vidrio))
    .ForMember(d => d.Fecha, o => o.MapFrom(m => m.DateUpdated))
    .ForMember(d => d.PapelYCarton, o => o.MapFrom(m => m.Papel));
    #endregion

  }

}