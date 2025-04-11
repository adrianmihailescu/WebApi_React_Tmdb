using Microsoft.EntityFrameworkCore;
using MovieCommentsApi.Data;
using MovieCommentsApi.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Register IHttpClientFactory first to ensure it is available to services that depend on it
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllers();

// Add JWT Authentication
var secretKeyFromConfig = builder.Configuration["Jwt:SecretKey"];
Console.WriteLine($"Secret Key from Configuration: {secretKeyFromConfig}"); 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),  // Secret key
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // The issuer of the token
            ValidAudience = builder.Configuration["Jwt:Audience"] // The audience for the token
        };
    });

// Add authorization
builder.Services.AddAuthorization();

// Register the EF Core DbContext using connection string (ensure the connection string is correct)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services for movie data and comments
builder.Services.AddScoped<IMovieService, MovieService>();  // Service that calls themoviedb.org API
builder.Services.AddScoped<ICommentsService, CommentsService>();  // Local comments handling

// Register Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React app URL
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    // In Development, don't force HTTPS
    app.UseDeveloperExceptionPage(); // For debugging
}
else
{
    app.UseHttpsRedirection(); // Use this in production environments
}

// Use CORS middleware before using Authentication and Authorization middleware
app.UseCors("AllowAll");  // This must be placed before UseAuthentication and UseAuthorization

app.UseHttpsRedirection();
// Middleware to use authentication and authorization
app.UseAuthentication();

// Swagger for testing API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();  // Enables routing middleware
app.UseAuthorization();
app.UseEndpoints(endpoints =>   // Ensure endpoints are mapped correctly
{
    endpoints.MapControllers();  // Maps controller routes
});

app.MapControllers();

app.Run();
