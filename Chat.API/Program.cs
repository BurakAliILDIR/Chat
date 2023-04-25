using Chat.API;
using Chat.API.Extensions;
using Chat.API.Hubs;
using Chat.API.Infrastructure.Mail;
using Chat.API.Mapper;
using Chat.API.Middlewares;
using IdentityExample.Web.Extensions;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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

// SignalR
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
}).AddMessagePackProtocol();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .WithOrigins("https://localhost:7097", "http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

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

app.UseCors("CorsPolicy");

app.MapHub<MessageHub>("/messagehub");

app.Run();