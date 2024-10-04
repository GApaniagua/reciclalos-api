namespace Application.CollectorCore.Dto;




public class CreateCollectorMaterials 

{
  public int CollectorId { get; set; }
  public int CollectionCenterId { get; set; }
  public IEnumerable<MaterialsCollector> Materials { get; set; }  = null!;
}


public class MaterialsCollector 
{
  public string MaterialType { get; set; } = null!;
  public int Quantity { get; set; } = 0;
}
