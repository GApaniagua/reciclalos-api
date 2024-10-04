
using Microsoft.AspNetCore.Mvc;
using Application.CatalogCore.Dto;
using Domain.Catalog;
using API.Common.Response;
using API.Common.Enum;
using Domain.Material;
using System.Text;
using Application.CollectorCore.Dto;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;


namespace API.Controller;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
  private readonly ICatalogService _catalogService;
  public CatalogController(
    ICatalogService collectorManager
  )
  {
    this._catalogService = collectorManager;
  }

  [HttpGet("material/type")]
  [SwaggerOperation(Summary = "Obtains material type",
                  Description = "This service is public.")]

  public async Task<Response<IEnumerable<MaterialTypes>>> GetMaterialTypes()
  {

    return new Response<IEnumerable<MaterialTypes>>(
      StatusServerResponse.OK,
      await this._catalogService.GetAllMaterialTypesUseCase(),
      true,
      ""
    );
  }

  // [HttpGet("material/recyclable-type")]
  // public async Task<Response<IEnumerable<MaterialRecyclableType>>> GetMaterialRecyclableTypes()
  // {
  //   return new Response<IEnumerable<MaterialRecyclableType>>(
  //     StatusServerResponse.OK,
  //     await this._catalogService.GetAllMaterialRecyclableTypesUseCase(),
  //     true,
  //     ""
  //   );
  // }

  [HttpGet("departments")]
  [SwaggerOperation(Summary = "Obtains Departments",
                  Description = "This service is public.")]
  public async Task<Response<IEnumerable<Department>>> GetAllDepartments()
  {
    var departments = await this._catalogService.GetAllDepartmentsUseCase();
    return new Response<IEnumerable<Department>>(
      StatusServerResponse.OK,
      departments,
      true,
      ""
    );
  }

  [HttpGet("municipalities")]
  [SwaggerOperation(Summary = "Obtains Municipalities",
                  Description = "This service is public.")]
  public async Task<Response<IEnumerable<Municipality>>> GetAllMunicipalities()
  {
    var municipalities = await this._catalogService.GetAllMunicipalitiesUseCase();
    return new Response<IEnumerable<Municipality>>(
      StatusServerResponse.OK,
      municipalities,
      true,
      ""
    );
  }


  [Authorize(Policy = "AdminOnly")]
  [HttpGet("collector/collectortype")]
  [SwaggerOperation(Summary = "Obtains Collector Types",
                  Description = "This service can only be accessed by administrators.")]
  public async Task<Response<IEnumerable<CollectorTypeDto>>> GetCollectorType()
  {
    var collectorTypes = await this._catalogService.GetCollectorTypeUseCase();
    return new Response<IEnumerable<CollectorTypeDto>>(
      StatusServerResponse.OK,
      collectorTypes,
      true,
      ""
    );
  }

  [Authorize(Policy = "AdminOnly")]
  [HttpGet("report")]
  [SwaggerOperation(Summary = "Obtains the report of collected materials",
                  Description = "This service can only be accessed by administrators.")]
  public async Task<IActionResult> GetReports([FromQuery] int? materialTypeId, [FromQuery] string startDate, [FromQuery] string endDate)
  {
    DateTime start = DateTime.Parse(startDate);
    DateTime end = DateTime.Parse(endDate);
    var csv = await this._catalogService.GetReportUseCase(materialTypeId, start, end);

    byte[] csvBytes = Encoding.UTF8.GetBytes(csv);
    // Devolver el archivo CSV como respuesta
    return File(csvBytes, "text/csv", "registros_acopio.csv");
  }
}