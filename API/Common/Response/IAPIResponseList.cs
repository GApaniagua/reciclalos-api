
using System.Net;
public class IAPIResponseList<T>
{
    public HttpStatusCode Status { get; set; }
    public bool IsSuccess { get; set; } = true;
    public List<string> Message { get; set; }
    public List<T> Data { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int Total { get; set; }
}