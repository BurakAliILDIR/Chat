using Chat.API;
using Chat.API.Configs;
using Chat.API.Entities;
using Chat.API.Extensions;
using Chat.API.Infrastructure.Mail;
using Chat.API.Middlewares;
using IdentityExample.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomConfigs(config: builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<AppDbContext>());

builder.Services.AddDbConfiguration(config: builder.Configuration);

builder.Services.AddIdentityExtension();
builder.Services.AddJwtConfiguration(config: builder.Configuration);

builder.Services.AddScoped<IMailService, GoogleMailService>();

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