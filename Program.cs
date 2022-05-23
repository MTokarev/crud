using crud.Data;
using crud.Interfaces;
using crud.Options;
using crud.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    opt =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);

            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Crud documentation"
            });
            opt.SwaggerGeneratorOptions = new SwaggerGeneratorOptions
            {

            };
        }
);

// Adding data layer
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Binding options
builder
    .Services
    .Configure<PaginationOptions>(builder.Configuration.GetSection(nameof(PaginationOptions)));

// Configuring CORS policy to allow Angular client access the backend
string corsPolicyName = "AllowAngularClient";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            string[] corsUris = builder.Configuration.GetSection("CorsUris").Get<string[]>();
            policy
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins(corsUris);
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(corsPolicyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
