namespace Application.CollectionCenterCore.Dto;

public class CollectionCenterDto
{
    public required int id { get; set; }            // Ejemplo: "Coca-cola zona 17"
    public required string name { get; set; }            // Ejemplo: "Coca-cola zona 17"
    public string address { get; set; }         // Ejemplo: "Northwest"
    public string schedule { get; set; }        // Ejemplo: "Lunes a Viernes: 08:00 a 17:00 Sábado: 08:00 a 12:00"
    public string department { get; set; }      // Ejemplo: "Guatemala"
    public string municipality { get; set; }    // Ejemplo: "Guatemala"
    public IEnumerable<int> materials { get; set; } // Ejemplo: [1,2,3,4,5]
    public string partners { get; set; }        // Ejemplo: "Municipalidad de Guatemala"
    public string phone { get; set; }           // Ejemplo: "(509) 706-5959 x21594"
    public string phone2 { get; set; }          // Ejemplo: "1-874-235-5221"
    public string whatsapp { get; set; }        // Ejemplo: "361.719.6982 x849"
    public string email { get; set; }           // Ejemplo: "Chaya.Schroeder17@gmail.com"
    public string station { get; set; }         // Ejemplo: "Publico"
    public string mapUrl { get; set; }          // Ejemplo: "http://fitting-bet.biz"
    public string latitude { get; set; }
    public string longitude { get; set; }
}