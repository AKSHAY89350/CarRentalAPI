using Data_Assess_Layer;
using Microsoft.EntityFrameworkCore;
using Business_Layer;
using Data_Assess_Layer.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});
builder.Services.AddSwaggerGen(c =>
{

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
//Adding Authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})// Adding Jwt Bearer
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = configuration["JWT:ValidAudience"],
//        ValidIssuer = configuration["JWT:ValidIssuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
//    };
//});
//Add services to the container.
builder.Services.AddCors(c => c.AddPolicy("default", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
builder.Services.AddControllers();
builder.Services.AddDbContext<CarRentalDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
        });
});
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IRentalAgreementService, RentalAgreementService>();
builder.Services.AddScoped<IUserAuthentication, UserAuthentication>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRentalPriceService, RentalPriceService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<KeyRotationService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Ensure the token was issued by a trusted issuer
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // The expected issuer value from configuration
        ValidateAudience = false, // Disable audience validation (can be enabled as needed)
        ValidateLifetime = true, // Ensure the token has not expired
        ValidateIssuerSigningKey = true, // Ensure the token's signing key is valid
        // Define a custom IssuerSigningKeyResolver to dynamically retrieve signing keys from the JWKS endpoint
        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
        {
            Console.WriteLine($"Received Token: {token}");
            Console.WriteLine($"Token Issuer: {securityToken.Issuer}");
            Console.WriteLine($"Key ID: {kid}");
            Console.WriteLine($"Validate Lifetime: {parameters.ValidateLifetime}");
            var httpClient = new HttpClient();
            var jwks = httpClient.GetStringAsync($"{builder.Configuration["Jwt:Issuer"]}/.well-known/jwks.json").Result; // Parse the fetched JWKS into a JsonWebKeySet object
            var keys = new JsonWebKeySet(jwks);
            // Return the collection of JsonWebKey objects for token validation
            return keys.Keys;
        }
    };
});


builder.Logging.ClearProviders();
builder.Logging.AddConsole();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

app.Run();