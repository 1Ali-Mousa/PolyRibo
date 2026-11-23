using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace PolyRibo.API.Transformer;

public class DefaultResponsesOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        operation.Responses ??= new OpenApiResponses();

        operation.Responses.TryAdd("200", new OpenApiResponse
        {
            Description = "OK"
        });

        operation.Responses.TryAdd("201", new OpenApiResponse
        {
            Description = "Created"
        });

        operation.Responses.TryAdd("400", new OpenApiResponse
        {
            Description = "Bad Request"
        });

        operation.Responses.TryAdd("401", new OpenApiResponse
        {
            Description = "Unauthorized"
        });

        operation.Responses.TryAdd("403", new OpenApiResponse
        {
            Description = "Forbidden"
        });

        operation.Responses.TryAdd("404", new OpenApiResponse
        {
            Description = "Not Found"
        });

        operation.Responses.TryAdd("409", new OpenApiResponse
        {
            Description = "Conflict"
        });

        operation.Responses.TryAdd("429", new OpenApiResponse
        {
            Description = "Too Many Requests"
        });

        operation.Responses.TryAdd("500", new OpenApiResponse
        {
            Description = "Server Error"
        });

        return Task.CompletedTask;
    }
}