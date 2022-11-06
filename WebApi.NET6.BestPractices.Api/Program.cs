using BestPractices.API.Authentication.Extensions;
using BestPractices.API.Controllers.Common;
using BestPractices.API.Extensions;
using BestPractices.API.Filters;
using BestPractices.API.Middleware;
using BestPractices.API.Problems;
using BestPractices.Common.Exceptions;
using BestPractices.Core;
using BestPractices.Core.Account.Data;
using BestPractices.Core.Book.Data;
using BestPractices.Core.Book.Dto;
using BestPractices.Core.Book.Query;
using Cqrs.Events;
using Cqrs.Hosts;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. + Mapping StatuCodes
builder.Services.AddControllers(options =>
{
    // Managing exceptions
    options.Filters.Add<ExternalExceptionFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    options.ClientErrorMapping[StatusCodes.Status500InternalServerError] = new ClientErrorData()
    {
        Link = "https://httpstatuses.com/500",
        Title = "TITOLO CUSTOM STATUS 500"
    };
    options.ClientErrorMapping[StatusCodes.Status401Unauthorized].Link = "https://httpstatuses.com/401";
}
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Changed AddSwagger for adding BearerToken Authorization
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Book API",
        Description = "ASP.NET Core Web API for managing books",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact()
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        }
    });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
    // Enable XML Comments
    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    options.IncludeXmlComments(xmlPath);
});

// Inject MediatR (NOT USING AUTOFAC)
builder.Services.AddMediatR(typeof(Program));
// Inject clean class for loading Assembly
builder.Services.AddMediatR(typeof(CoreModule).GetTypeInfo().Assembly);
// Load FakeData Custom Extension
builder.Services.LoadFakeData();
// Enabling Authorization with JWT
builder.Services.AddJWTTokenServices(builder.Configuration);
// Override Problem with Custom
builder.Services.AddTransient<ProblemDetailsFactory, SampleProblemsFactory>();
// Logger
builder.Services.AddLogging();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/errorhandler/error-development");
}
else
{
    // ONLY HANDLE ERRORS - LESS FLEXIBLE - MORE PERFORMANCE 
    //app.AddErrorHandler(app.Services.GetService<ILogger<IBestPracticesException>>());

    // HANDLES EVERYTHING - MORE PROCESSING - LESS PERFORMANCE - MORE FLEXIBLE
    app.UseMiddleware<CustomErrorMiddleware>(app.Services.GetService<ILogger<IBestPracticesException>>());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
