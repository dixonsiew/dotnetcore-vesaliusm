using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using System.Text;
using vesalius_m.Services;
using vesalius_m.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCors(c =>
{
    c.AddPolicy("vesm", c =>
    {
        c.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("Authorization", "filename", Constants.X_TOTAL_COUNT, Constants.X_TOTAL_PAGE);
    });
});

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
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });

});
builder.Services.AddScoped<DefaultConnection>();
AddServices(builder.Services);
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
    };
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

app.UseStaticFiles();
app.UseSerilogRequestLogging();

string basePath = "vesm";
app.UsePathBase($"/{basePath}");
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler(e =>
{
    e.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var exception = contextFeature.Error;

            // Set status code based on exception
            context.Response.StatusCode = exception switch
            {
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = context.Response.StatusCode,
                message = exception.Message,
            });
        }
    });
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"/{basePath}/swagger/v1/swagger.json", "Vesalius-m API V1");
    c.DocumentTitle = "Swagger Vesalius-m API";
    c.InjectStylesheet("/css/theme-flattop.css");
    c.EnablePersistAuthorization();
});

app.MapControllers();

app.Run();

void AddServices(IServiceCollection services)
{
    services.AddScoped<ApplicationUserService>();
    services.AddScoped<AdminUserService>();
    services.AddScoped<TokenService>();
    services.AddScoped<TokenAdminService>();
    services.AddScoped<AuthService>();
    services.AddScoped<AppService>();
    services.AddScoped<CountryService>();
}