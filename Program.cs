#region using
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AparmentSystemAPI.Models.Payments;
using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Payments.Interfaces;
using AparmentSystemAPI.Models.Flats;
using AparmentSystemAPI.Models.Flats.Interfaces;
using AparmentSystemAPI.Models.Identities.Interfaces;
using AparmentSystemAPI.Models;
using AparmentSystemAPI.Models.UnitOfWorks;
using AparmentSystemAPI.Models.Tokens;
using AparmentSystemAPI.Models.MainBuildings.Interfaces;
using AparmentSystemAPI.Models.MainBuildings;
#endregion


var builder = WebApplication.CreateBuilder(args);

#region avoid circular reference
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#endregion

#region Swagger configuration
//builder.Services.AddSwaggerGen();
//// Swagger konfigürasyonu
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

    // JWT Authentication için Swagger konfigürasyonu
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion

// Add services and repositories to the container.
# region Add services and repositories to the container
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IFlatService, FlatService>();
builder.Services.AddScoped<IFlatRepository, FlatRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IMainBuildingRepository, MainBuildingRepository>();
builder.Services.AddScoped<IMainBuildingService, MainBuildingService>();
#endregion


builder.Services.AddDbContext<AppDbContext>(options =>
{     options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<AppDbContext>();

# region authentication

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

# endregion

#region Middleware
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}
app.UseSwagger();
app.UseSwaggerUI();


// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// To run the data extraction process
await SeedDataAsync(app.Services);

app.Run();
#endregion

#region SeedDataAsync
async Task SeedDataAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

    await DataSeeder.SeedData(roleManager,userManager);
    await DataSeeder.SeedMainBuilding(serviceProvider.GetRequiredService<AppDbContext>());
}
#endregion