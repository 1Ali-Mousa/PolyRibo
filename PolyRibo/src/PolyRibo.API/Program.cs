using PolyRibo.API.Middleware;
using PolyRibo.Application.DependencyInjection;
using PolyRibo.Infrastructure.DependencyInjection;
using PolyRibo.API.Transformer;
using PolyRibo.API.Extension;

var builder = WebApplication.CreateBuilder(args);

//Controllers
builder.Services.AddControllers();

builder.Services.AddApiBehaviorConfigurationExtension();
//OpenAPI
builder.Services.AddOpenApi(options =>
{
    options.AddOperationTransformer<DefaultResponsesOperationTransformer>();
});
// Application & Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();

// Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "PolyRibo API v1"
        );
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
