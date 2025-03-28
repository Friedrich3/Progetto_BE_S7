using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Progetto_BE_S7.Data;
using Progetto_BE_S7.Models.Auth;
using Progetto_BE_S7.Services;
using Progetto_BE_S7.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = builder.Configuration.GetSection("Identity").GetValue<bool>("SignInRequireConfirmedAccount");
    options.Password.RequiredLength = builder.Configuration.GetSection("Identity").GetValue<int>("RequiredLength");
    options.Password.RequireDigit = builder.Configuration.GetSection("Identity").GetValue<bool>("RequireDigit");
    options.Password.RequireLowercase = builder.Configuration.GetSection("Identity").GetValue<bool>("RequireLowercase");
    options.Password.RequireUppercase = builder.Configuration.GetSection("Identity").GetValue<bool>("RequireUppercase");
    options.Password.RequireNonAlphanumeric = builder.Configuration.GetSection("Identity").GetValue<bool>("RequireNonAlphanumeric");
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<Jwt>(builder.Configuration.GetSection(nameof(Jwt)));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("Issuer"),
        ValidAudience = builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("SecurityKey")))
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserManager<ApplicationUser>>();		
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<ApplicationRole>>();

builder.Services.AddScoped<ArtistaServices>();
builder.Services.AddScoped<EventoServices>();
builder.Services.AddScoped<BigliettoServices>();

var app = builder.Build();

app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
