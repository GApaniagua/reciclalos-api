using System.Net;

namespace API.Common.Response;

public class Response<TBody>
{
    public HttpStatusCode Status { get; set; }
    public TBody Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public Response()
    {
        Message = string.Empty;
    }
    public Response(HttpStatusCode _status, TBody _data, bool _isSuccess, string _message)
    {
        Status = _status;
        Data = _data;
        IsSuccess = _isSuccess;
        Message = _message;
    }
}

