using DataBase;
using DataBase.Repositories;
using domain.Logic;
using domain.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApp.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
{
  options.UseNpgsql($"Host=localhost;Port=5432;Database=db;Username=postgres;Password=9564")
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<UserService>();

builder.Services.AddTransient<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddTransient<SpecializationService>();

builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<DoctorService>();

builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddTransient<AppointmentService>();

builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();
builder.Services.AddTransient<ScheduleService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidIssuer = AuthOptions.ISSUER,
      ValidateAudience = true,
      ValidAudience = AuthOptions.AUDIENCE,
      ValidateLifetime = true,
      IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
      ValidateIssuerSigningKey = true,
    };
  });
builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(opt =>
{
  opt.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });
  opt.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
  {
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Scheme = "bearer"
  });
  opt.OperationFilter<AuthenticationRequirementsOperationFilter>();
});

var app = builder.Build();

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