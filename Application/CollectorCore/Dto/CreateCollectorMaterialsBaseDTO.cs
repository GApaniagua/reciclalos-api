using System.ComponentModel.DataAnnotations;

namespace Application.CollectorCore.Dto;




public class CreateCollectorMaterialsDTO

{
  [Required]
  public int CollectorId { get; set; }
  [Required]
  public int CollectionCenterId { get; set; }
  [Required]
  public List<MaterialsCollectorDTO> Materials { get; set; }
}


public class MaterialsCollectorDTO
{
  [Required]
  public string MaterialType { get; set; }
  [Required]
  public int Quantity { get; set; }
}
