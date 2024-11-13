using InsideSistemas.Api;
using InsideSistemas.Api.Middlewares;
using InsideSistemas.Application;
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddApplication(builder.Configuration);

var app = builder.Build();
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
