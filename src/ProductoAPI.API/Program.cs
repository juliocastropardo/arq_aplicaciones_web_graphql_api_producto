using Microsoft.EntityFrameworkCore;
using ProductoAPI_GraphQL.Application.Commands;
using ProductoAPI_GraphQL.Application.Validators;
using ProductoAPI_GraphQL.Application.Behaviors;
using ProductoAPI_GraphQL.Domain.Repositories;
using ProductoAPI_GraphQL.Infrastructure.Data;
using ProductoAPI_GraphQL.Infrastructure.Repositories;
using ProductoAPI_GraphQL.API.GraphQL.Queries;
using ProductoAPI_GraphQL.API.GraphQL.Mutations;
using System.Reflection;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Producto API",
        Version = "v1",
        Description = "API RESTful para gestión de productos con arquitectura limpia, CQRS y patrón repositorio. " +
                      "Esta API permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre productos.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Producto API",
            Email = "support@ProductoAPI_GraphQL.com"
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    
    // Incluir comentarios XML si están disponibles
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
    
    // Configurar esquemas para mejor documentación
    c.CustomSchemaIds(type => type.FullName);
});

// Database Configuration - Read and Write connections
var readConnection = builder.Configuration.GetConnectionString("ReadConnection");
var writeConnection = builder.Configuration.GetConnectionString("WriteConnection");

builder.Services.AddDbContext<ReadDbContext>(options =>
    options.UseSqlServer(readConnection));

builder.Services.AddDbContext<WriteDbContext>(options =>
    options.UseSqlServer(writeConnection));

// Repository Pattern
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductoCommandValidator>();

// MediatR for CQRS
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(typeof(CreateProductoCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// GraphQL Configuration
builder.Services
    .AddGraphQLServer()
    .AddQueryType<ProductoQuery>()
    .AddMutationType<ProductoMutation>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
// Swagger siempre disponible (debe ir antes de otros middlewares)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Producto API V1");
    c.RoutePrefix = "swagger"; // Swagger UI en /swagger
    c.DisplayRequestDuration();
    c.EnableDeepLinking();
    c.EnableFilter();
    c.ShowExtensions();
    c.EnableValidator();
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
});

app.UseHttpsRedirection();

app.UseAuthorization();

// Global Exception Handler Middleware (debe ir después de UseAuthorization pero antes de MapControllers)
app.UseMiddleware<ProductoAPI_GraphQL.API.Middleware.ExceptionHandlingMiddleware>();

app.MapControllers();

// Map GraphQL endpoint
app.MapGraphQL();

// Ensure database is created (Entity Framework Core creará las tablas automáticamente)
using (var scope = app.Services.CreateScope())
{
    var writeDbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    try
    {
        var created = await writeDbContext.Database.EnsureCreatedAsync();
        if (created)
        {
            logger.LogInformation("✓ Tabla 'Productos' creada exitosamente en WriteDbContext");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error al inicializar la base de datos: {Message}", ex.Message);
    }
}

app.Run();

