using BackEnd.DataAccess;
using BackEnd.Models.DAL;
using BackEnd.Models.Domains;
using BackEnd.Services.Implements;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDbContext<AppDBContext>(options => {
    var connectionString = configuration.GetConnectionString("default");
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDBContext>();

builder.Services.AddCors(policy => {
    policy.AddPolicy("defaultPolicy", options => {
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowAnyOrigin();
    });
});

builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationRoomService, ReservationRoomService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel API", Version = "v1" });
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme",
    });

    var openApiReferencce = new OpenApiReference
    {
        Type = ReferenceType.SecurityScheme,
        Id = "Bearer",
    };

    var securityScheme = new OpenApiSecurityScheme { Reference = openApiReferencce };
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new List<string>() }
    });
});

var key = Encoding.UTF8.GetBytes(builder.Configuration["ApplicationSettings:JWT_Secret"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateIssuer = false,
        ValidateAudience = false,

        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("defaultPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
