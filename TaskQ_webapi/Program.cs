using Microsoft.EntityFrameworkCore;
using TaskQ_webapi.Data;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.Tokens.Experimental;
using Microsoft.OpenApi;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// ------------------ SERVICES ------------------

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbcontext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,

//            ValidIssuer = builder.Configuration["JWT:Issuer"],
//            ValidAudience = builder.Configuration["JWT:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])
//                )
//        };
//    }
//    );

//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Demo Web API",
//        Version = "v1"
//    });

//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Name = "Authorization",
//        Description = "Enter: Bearer {your JWT token}"
//    });

//    options.AddSecurityRequirement(document =>
//    {
//        return new OpenApiSecurityRequirement
//        {
//            {
//                new OpenApiSecuritySchemeReference("Bearer"),
//                new List<string>()
//            }
//        };
//    });
//});

// CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// ------------------ DATABASE MIGRATION ------------------
// 🔥 This ensures DB + tables always exist on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbcontext>();
    db.Database.Migrate();
}

// ------------------ MIDDLEWARE ------------------

app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

