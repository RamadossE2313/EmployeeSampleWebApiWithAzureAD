using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// adding azure add authentication
//JwtBearerDefaults.AuthenticationScheme we are mentioned schema as bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // it will get azure ad details from the app setting json
    .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));  

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EmployeeSampleWebApi",
        Description ="This is the sample web api application with azure ad and swagger",
        Version = "v1"
    });

    // here we mentioning how our api is protected,
    // when we enable this section only we can see the auhorize button in the swagger page

    c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        Description = "Azure ad authentication using oauth 2.0 using authorization code flow (PKCE)",
        Name = "OAuth2 Azure ad authentication",
        // we are using OAuth2 authentication type
        Type = SecuritySchemeType.OAuth2,
        // we have 4 flows (Implicit,Password,ClientCredentials and AuthorizationCode), if you want to know about it
        // right OpenApiOAuthFlows and go to definition see those flows
        Flows = new OpenApiOAuthFlows
        {
            // we are using authorizationcode flow 
            AuthorizationCode = new OpenApiOAuthFlow
            {
                // authoriation url 
                AuthorizationUrl = new Uri($"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
                // token url
                TokenUrl = new Uri($"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                {
                    { $"api://{configuration["AzureAd:ClientId"]}/{configuration["AzureAd:Scopes"]}", "Access API as user"}
                }
            }
        }
    });

    // here we are defining how to security requirement should be (needs to learn swagger course to get more inforamtion)
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "OAuth2"
                }
            },
            new[]
            {
                $"api://{configuration["AzureAd:ClientId"]}/{configuration["AzureAd:Scopes"]}"
            }
        }

    });;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // client app id
        c.OAuthClientId(configuration["AzureAd:SwaggerClientId"]);
        // we are using pcke type 
        c.OAuthUsePkce();
        // multiple scope found means we are separting based on space
        c.OAuthScopeSeparator(" ");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
