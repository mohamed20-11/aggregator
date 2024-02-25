
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 7244, o => o.Protocols = HttpProtocols.Http2);

    // ADDED THIS LINE to fix the problem
    options.Listen(IPAddress.Any, 5051, o => o.Protocols = HttpProtocols.Http1);
    //  options.Listen(IPAddress.Any, 5041, o => o.Protocols = HttpProtocols.Http1);
});

builder.Services.AddServices();
builder.Services.AddCorsConfig();
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddGrpcConfig(builder.Configuration);
builder.Services.AddSwaggerConfig();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "/wwwroot"
});

app.UseSwagger();
app.UseSwaggerUI();

//app.MapGrpcService<ServicePermissionTicketingSystemAggregator>();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapGrpcReflectionService();
app.UseCors("AllowCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    throw ex;
}
