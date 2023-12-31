var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddCoreInfrastructure(builder.Configuration);

// Mediator        
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserHandler).Assembly));

// Services
services.AddScoped<ITokenRequester, TokenRequester>();
services.AddScoped<IEmailUniquenessChecker, EmailUniquenessChecker>();
services.AddScoped<IEventStoreRepository<User>, MartenRepository<User>>();
services.AddMarten(builder.Configuration,
    options => options.ConfigureProjections());

// Policies
services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyBuilder.ReadPolicy, PolicyBuilder.ReadAccess);
    options.AddPolicy(PolicyBuilder.WritePolicy, PolicyBuilder.WriteAccess);
});

// App
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseSwagger(builder.Configuration);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
