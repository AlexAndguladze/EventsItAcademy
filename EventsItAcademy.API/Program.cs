using EventsItAcademy.API.Infrastructure.Auth.JWT;
using EventsItAcademy.API.Infrastructure.Extensions;
using EventsItAcademy.API.Infrastructure.Middlewares;
using EventsItAcademy.API.Infrastructure.VersionSwagger;
using EventsItAcademy.Domain.Users;
using EventsItAcademy.Persistence.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(1, 0);
    option.AssumeDefaultVersionWhenUnspecified = true;

    //option.ApiVersionReader = ApiVersionReader.Combine
    //(
    //    //new QueryStringApiVersionReader("api-lasha")
    //    //new HeaderApiVersionReader("x-version")
    //    //new MediaTypeApiVersionReader("version")
    //);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddVersionedApiExplorer(option =>
{
    option.GroupNameFormat = "'v'VVV";
    option.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(option =>
{
    option.OperationFilter<SwaggerDefaultValues>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine($"{AppContext.BaseDirectory}", xmlFile);
    option.IncludeXmlComments(xmlPath);
    option.ExampleFilters();

    option.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        //Type = SecuritySchemeType.Http,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "basic"
                  }
              },
              new string[] {}
            }
        });
});
//swaggerExamples
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());
//swagger configure options
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

//dbContext
var connectionString = builder.Configuration.GetConnectionString("EventsItAcademyDbContextConnection") ?? throw new InvalidOperationException("Connection string 'EventsItAcademyDbContextConnection' not found.");

builder.Services.AddDbContext<EventsItAcademyDbContext>(options =>
    options.UseSqlServer(connectionString));
//identity
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventsItAcademyDbContext>();
//jwt
builder.Services.AddTokenAuthentication(builder.Configuration.GetSection(nameof(JWTConfiguration)).GetSection(nameof(JWTConfiguration.Secret)).Value);

builder.Services.Configure<JWTConfiguration>(builder.Configuration.GetSection(nameof(JWTConfiguration)));

//my services
builder.Services.AddServices();

//healthCheck
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("EventsItAcademyDbContextConnection"));

//fluent validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
//seeding
AppDbInitializer.Seed(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(option =>
    {
        foreach (var desciptions in provider.ApiVersionDescriptions)
        {
            option.SwaggerEndpoint($"/swagger/{desciptions.GroupName}/swagger.json"
                , $"{desciptions.GroupName.ToUpper()}");
        }
    });
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health", new HealthCheckOptions()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    endpoints.MapControllers();
});
app.MapControllers();
app.UseRequestCulture();
try
{
    Log.Information("Staring web host");
    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Host terminated");
}
finally
{
    Log.CloseAndFlush();
}

