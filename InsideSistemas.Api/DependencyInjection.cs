using InsideSistemas.Api.GraphQL;

namespace InsideSistemas.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            // Add HotChocolate GraphQL Configuration
            services
                .AddGraphQLServer()
                .AddQueryType<PedidoQuery>()
                .AddMutationType<PedidoMutation>()
                .AddFiltering()
                .AddSorting()
                .AddProjections();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
