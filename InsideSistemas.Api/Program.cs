using InsideSistemas.Api.GraphQL;
using InsideSistemas.Application;

var builder = WebApplication.CreateBuilder(args);

// Add All Services
builder.Services.AddAllServices("My connectionString here");

// Add HotChocolate GraphQL Configuration
builder.Services
    .AddGraphQLServer()
    .AddQueryType<PedidoQuery>()
    .AddMutationType<PedidoMutation>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
