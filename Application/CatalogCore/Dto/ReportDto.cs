namespace Application.CatalogCore.Dto;
public class ReportDto
{
    public int IdCentroAcopio { get; set; }
    public string CentroDeAcopio { get; set; }
    public int Latas { get; set; }
    public int PapelYCarton { get; set; }
    public int PlasticoPet { get; set; }
    public int PlasticoOtros { get; set; }
    public int EnvasesVidrio { get; set; }
    public int TetraPak { get; set; }
    public DateTime Fecha { get; set; }
}