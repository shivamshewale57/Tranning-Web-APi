using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Web_Api.Data;
using Web_Api.Mappings;
using Web_Api.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

//New Changes (About DI)  add db context
builder.Services.AddDbContext<WebApiDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiConnectionString")));

// auth db context

builder.Services.AddDbContext<WebApiAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiAuthConnectionString")));


// inject repository pattern 
builder.Services.AddScoped<IRegionRepository, SQlRegionRepository>();

builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();







// inject Auto mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//add Authantiation to services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audiance"],
        IssuerSigningKey= new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Images")),
    RequestPath= "/Images"
});

app.MapControllers();

app.Run();
