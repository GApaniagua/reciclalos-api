namespace API.Common.Enum;

public enum StatusServerResponse
{
  // Respuesta exitosa
  OK = 200,

  // El recurso ha sido creado exitosamente
  Created = 201,

  // El servidor ha aceptado la solicitud pero no la ha procesado completamente
  Accepted = 202,

  // Solicitud mal formada
  BadRequest = 400,

  // No autorizado
  Unauthorized = 401,

  // El recurso solicitado no fue encontrado
  NotFound = 404,

  // Error en el servidor
  InternalServerError = 500,

  // Servicio no disponible
  ServiceUnavailable = 503

}