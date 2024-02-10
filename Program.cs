using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//-------------------
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using AparmentSystemAPI.Models.Payments;
using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Payments.Interfaces;
using AparmentSystemAPI.Models.Flats;
using AparmentSystemAPI.Models.Flats.Interfaces;
using AparmentSystemAPI.Models.Identities.Interfaces;
using AparmentSystemAPI.Models;
using AparmentSystemAPI.Models.UnitOfWorks;
using AparmentSystemAPI.Models.Tokens;
//-------------------


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<TokenService>();
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//// Swagger konfigürasyonu
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

//    // JWT Authentication için Swagger konfigürasyonu
//    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
//    });

//    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//    {
//        {
//            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//            {
//                Reference = new Microsoft.OpenApi.Models.OpenApiReference
//                {
//                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });
//});


builder.Services.AddAutoMapper(typeof(Program));



//unitofwork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFlatService, FlatService>();
builder.Services.AddScoped<IFlatRepository, FlatRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();




builder.Services.AddDbContext<AppDbContext>(options =>
{     options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    //schema

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var signatureKey = builder.Configuration.GetSection("TokenOptions")["SignatureKey"]!;
    var issuer = builder.Configuration.GetSection("TokenOptions")["Issuer"]!;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidIssuer = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey))
    };
});

//// JWT Authentication ve Authorization Yapýlandýrmasý
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        var issuer = builder.Configuration.GetSection("TokenOptions")["Issuer"]!;
//        var signatureKey = builder.Configuration.GetSection("TokenOptions")["SignatureKey"]!;

//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey)),
//            ValidateIssuer = true,
//            ValidateAudience = false, // Audience doðrulanmayacaksa false olarak ayarlayýn
//            ValidIssuer =issuer,
//            // Audience doðrulamasý yapýlacaksa "ValidAudience" deðerini ekleyin
//           // ValidAudience = "your_audience",
//            ValidateLifetime = true, // Token ömrünün doðrulanmasýný istiyorsanýz true olarak ayarlayýn
//            ClockSkew = TimeSpan.Zero // Token süresi dolduðunda hemen geçersiz sayýlmasý için
//        };
//    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}
app.UseSwagger();
app.UseSwaggerUI();

//call  CreatePrimaryAdminUser method from TokenService.cs
//var tokenService = app.Services.GetRequiredService<TokenService>();

// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// To run the data extraction process
await SeedDataAsync(app.Services);

app.Run();

async Task SeedDataAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

    await DataSeeder.SeedData(roleManager,userManager);
    await DataSeeder.SeedFlat(serviceProvider.GetRequiredService<AppDbContext>());
}
