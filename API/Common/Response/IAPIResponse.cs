
using System.Net;
public class IAPIResponse<T>
{
    public HttpStatusCode Status { get; set; }
    public bool IsSuccess { get; set; } = true;
    public List<string> Message { get; set; }
    public T Data { get; set; }
}