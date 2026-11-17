namespace PolyRibo.API.Response;

public class APIResponse<T>
{
    public int statusCode { get; set; }
    public string message { get; set; } = string.Empty;
    public T? data { get; set; }
}
