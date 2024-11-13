using InsideSistemas.Api.GraphQL;
using InsideSistemas.Api.Middlewares;
using InsideSistemas.Application;

var builder = WebApplication.CreateBuilder(args);

// Add All Services
builder.Services.AddAllServices("InsideSistemasDBInMemory");

// Add HotChocolate GraphQL Configuration
builder.Services
    .AddGraphQLServer()
    .AddQueryType<PedidoQuery>()
    .AddMutationType<PedidoMutation>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Add middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
    endpoints.MapControllers();
});

app.Run();
