using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

using TeamTaskManagement.Application.Interfaces.Repositories;
using TeamTaskManagement.Application.Interfaces.Services;
using TeamTaskManagement.Application.Services;

using TeamTaskManagement.Infrastructure.Data;
using TeamTaskManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// Controllers
// =====================================================

builder.Services.AddControllers();


// =====================================================
// Swagger + JWT Configuration
// =====================================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Add JWT Bearer authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        Type = SecuritySchemeType.Http,

        Scheme = "Bearer",

        BearerFormat = "JWT",

        In = ParameterLocation.Header,

        Description =
            "Enter your JWT token.\n\n" +
            "Example:\n" +
            "Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    // Tell Swagger that APIs can use JWT authentication
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            Array.Empty<string>()
        }
    });
});


// =====================================================
// Database
// =====================================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// =====================================================
// JWT Authentication
// =====================================================

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    });


// =====================================================
// Authorization
// =====================================================

builder.Services.AddAuthorization();


// =====================================================
// Repositories
// =====================================================

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITeamRepository, TeamRepository>();

builder.Services.AddScoped<IWorkItemRepository, WorkItemRepository>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();


// =====================================================
// Services
// =====================================================

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddScoped<IWorkItemService, WorkItemService>();

builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IDashboardService, DashboardService>();

// =====================================================
// CORS
// =====================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173/")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// =====================================================
// Build Application
// =====================================================

var app = builder.Build();


// =====================================================
// Swagger
// =====================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// =====================================================
// HTTP Pipeline
// =====================================================

app.UseHttpsRedirection();

app.UseCors("ReactPolicy");

// IMPORTANT: Authentication must come before Authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();