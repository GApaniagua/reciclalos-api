using API.Common.Enum;

namespace API.Common.Response;

public class Response<TBody>
{
    public StatusServerResponse status { get; set; }
    public TBody? data { get; set; }
    public bool isSuccess { get; set; }
    public string message { get; set; }

    public Response() {
        message = string.Empty;
    }
    public Response(StatusServerResponse _status, TBody? _data, bool _isSuccess, string _message)
    {
        status = _status;
        data = _data;
        isSuccess = _isSuccess;
        message = _message;
    }
}

