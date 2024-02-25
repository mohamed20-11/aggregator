using DynamicWorkflow.Aggregator.Helpers.WorkFlow;
using DynamicWorkflow.Aggregator.Services.Requesttemplate;

namespace DynamicWorkflow.Aggregator.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionMiddleware, GlobalExceptionMiddleware>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IResponseHelper, ResponseHelper>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRequestTemplateService, RequestTemplateService>();
            services.AddScoped<IEscalationLevel, EscalationLevel>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IWorkflowDataHelper, WorkFlowDataHelper>();
            services.AddScoped<IRquestTemplatehelper, RquestTemplatehelper>();

        }
        public static void AddGrpcConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();
            services.AddGrpcReflection();
            services.AddControlPanelAggGrpcClients(configuration["GrpcServiceSettings:ControlPanelAggUrl"]);
            services.AddSlaGrpcClients(configuration["GrpcServiceSettings:SlaUrl"]);
            services.AddDynamicWorkFlowGrpcClients(configuration["GrpcServiceSettings:DynamicWorkFlowUrl"]);
            services.AddOrgGrpcClients(configuration["GrpcServiceSettings:OrgUrl"]);

        }
        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors",
                    builder =>
                    {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
                    });
            });
        }

        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.AddSecurityDefinition(
                "token",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization
                }
                    );
                c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
                }
                    );
            });
        }
        public static void AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        // the name of your api resources   
                        options.Audience = "UserManagementServer";
                        /// identity server url                    
                        options.Authority = configuration.GetValue<string>("IdentityUrl");
                        options.RequireHttpsMetadata = false;
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ResourceOwnerPolicy", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireClaim("scope", "4EMicroServices"); // Require scope associated with Resource Owner Password token
                });

                options.AddPolicy("ClientCredentialsPolicy", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireClaim("scope", "CPAggregatorAPI"); // Require scope associated with Client Credentials token
                });
            });
        }


    }
}
