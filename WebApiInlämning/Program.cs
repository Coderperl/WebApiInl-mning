using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApiInlämning;
using WebApiInlämning.Data;
using WebApiInlämning.Infrastructure.Profiles;
var SecretKey = "LSDPl3okf92j4+0sd0kpow";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            ValidIssuer = "",
            ValidAudience = "",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey))
        };
    });

builder.Services.AddControllers(options =>
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter())
).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<DataInitializer>();
builder.Services.AddAutoMapper(typeof(AdvertisementProfile));


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitializer>().SeedData();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
