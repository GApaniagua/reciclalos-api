using AutoMapper;
using Infrastructure.Persistence.Interface;
using Domain.CollectionCenter;
using Application.CollectionCenterCore.Dto;
using Domain.Material;
using System;

namespace Application.CollectionCenterCore;

public class CollectionCenterManager : ICollectionCenterService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CollectionCenterManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<CollectionCenterDto>> GetAllCollectionCentersUseCase()
    {
        try
        {
            var entities = await this._unitOfWork.Repository.LocationRepository.GetAllAsync( q => q.Active == true);
            var originalCollectionCenters = _mapper.Map<IEnumerable<CollectionCenter>>(entities);

            var collectionCenters = _mapper.Map<IEnumerable<CollectionCenterDto>>(originalCollectionCenters);
            var materials = await this._unitOfWork.Repository.MaterialRepository.GetAllAsync();

            foreach (var collectionCenter in collectionCenters)
            {
                var originalCollectionCenter = originalCollectionCenters.Where(p => p.Id == collectionCenter.id).FirstOrDefault();

                if (originalCollectionCenter != null)
                {
                    List<int> materialsIds = [];
                    foreach (var material in originalCollectionCenter.Materials.Split(','))
                    {
                        var currentMaterial = materials.Where(p => p.Name.ToLower() == material.ToLower()).FirstOrDefault();

                        if (currentMaterial != null)
                        {
                            materialsIds.Add(currentMaterial.Id);
                        }
                    }

                    collectionCenter.materials = materialsIds;
                }
            }

            return collectionCenters;

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CollectionCenterDto?> GetCollectionCenterByIdUseCase(int id)
    {
        try
        {
            var entities = await this._unitOfWork.Repository.LocationRepository.SingleOrDefaultAsync(x => x.Id == id && x.Active == true);
            var dtos = _mapper.Map<CollectionCenter>(entities);

            if (dtos == null)
            {
                return null;
            }

            var collectionCenter = _mapper.Map<CollectionCenterDto>(dtos);
            var materials = await this._unitOfWork.Repository.MaterialRepository.GetAllAsync();

            List<int> materialsIds = [];
            foreach (var material in dtos.Materials.Split(','))
            {
                var currentMaterial = materials.Where(p => p.Name.ToLower() == material.ToLower()).FirstOrDefault();

                if (currentMaterial != null)
                {
                    materialsIds.Add(currentMaterial.Id);
                }
            }

            collectionCenter.materials = materialsIds;


            return collectionCenter;
        }
        catch (Exception)
        {
            throw;
        }
    }


    // public void DeterminateMaterialId(MaterialEnum material, Material )
    // {
    //  foreach (var material in materials)
    //     {
    //       switch (material.MaterialType)
    //       {
    //         case "Latas":

    //           break;
    //         case "Papel y cartón":
    //           collection.Papel -= material.Quantity;
    //           break;
    //         case "Plasticos PET":
    //           collection.PlasticoPet -= material.Quantity;
    //           break;
    //         case "Plasticos Otros":
    //           collection.PlasticoOtros -= material.Quantity;
    //           break;
    //         case "Envases y botellas de vidrio":
    //           collection.Vidrio -= material.Quantity;
    //           break;
    //         case "Multicapa":
    //           collection.Tetrapak -= material.Quantity;
    //           break;
    //         default:
    //           Console.WriteLine("Material no reconocido: " + material.MaterialType);
    //           break;
    //       }
    //     }
    // }

}
