using Chat.API;
using Chat.API.Configs;
using Chat.API.Entities;
using Chat.API.Extensions;
using Chat.API.Infrastructure.Jwt;
using Chat.API.Infrastructure.Mail;
using Chat.API.Mapper;
using Chat.API.Middlewares;
using IdentityExample.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// appsetttings.json dosyasý için gelen modeller.
builder.Services.AddCustomConfigs(config: builder.Configuration);

// MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<AppDbContext>());

// DbContext ayarlarý.
builder.Services.AddDbConfiguration(config: builder.Configuration);

// Microsoft Identity ayarlarý.
builder.Services.AddIdentityExtension();

// JWT ayarlarý.
builder.Services.AddJwtConfiguration(config: builder.Configuration);

builder.Services.AddHttpContextAccessor();

// Auto Mapper.
builder.Services.AddAutoMapper(typeof(Mapper));

builder.Services.AddScoped<IMailService, MailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();