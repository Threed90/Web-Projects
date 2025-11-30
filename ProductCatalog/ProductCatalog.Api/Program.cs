using ProductCatalog.Services.Interfaces;
using ProductCatalog.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "ProductCatalog.Api",
        Description = "An API for exercice purposes... Does not contain any large data or meaningful bisness logic.",
        License = new Microsoft.OpenApi.OpenApiLicense
        {
            Name = "Use under MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        },
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "ProductCatalog.Api Support",
            Email = "any@mail.com"
        },
        Version = "v1"
    });

    c.IncludeXmlComments("ProductCatalogApiDocumentation.xml");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/api");
    app.UseSwaggerUI(option =>
    {
        option.RoutePrefix = "";
        option.SwaggerEndpoint("/api", "ProductCatalog.Api");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
