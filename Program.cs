using Oracle.ManagedDataAccess.Client;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Swagger Vesalius-m API",
        Version = "v1",
        Description = "Vesalius-m API"
    });
});
builder.Services.AddScoped<IDbConnection>(x => new OracleConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

string basePath = "vesm";
app.UsePathBase($"/{basePath}");
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"/{basePath}/swagger/v1/swagger.json", "Vesalius-m API V1");
    c.DocumentTitle = "Swagger Vesalius-m API";
});


app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.Run();
