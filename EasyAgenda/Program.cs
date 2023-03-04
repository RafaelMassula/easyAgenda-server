using EasyAgenda.Data;
using EasyAgenda.Data.Contracts;
using EasyAgenda.Data.DAL;
using EasyAgenda.Model;
using EasyAgendaService;
using EasyAgendaService.Contracts;
using EasyAgendaService.DAL;
using EasyAgendaService.Data.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<EasyAgendaContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
  swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
  {
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT Authorization header using the Bearer scheme." +
                  "Enter 'Bearer' [space] and then your token in the text input below. " +
                  "Example: Bearer HEADER:ALGORITHM & TOKEN TYPE.PAYLOAD:DATA.VERIFY SIGNATURE"
  });
  swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
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
      new string[]{}
    }
  });
});

builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy", builder => builder
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers(options =>
{
  options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
  options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(
    new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
      ReferenceHandler = ReferenceHandler.Preserve,
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    }));
}).AddNewtonsoftJson();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwtBearerOptions =>
    {
      jwtBearerOptions.RequireHttpsMetadata = false;
      jwtBearerOptions.SaveToken = true;
      jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
      };
    });


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUriService>(service =>
{
  var acessor = service.GetRequiredService<IHttpContextAccessor>();
  var request = acessor?.HttpContext?.Request;
  var baseUri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());

  return new UriService(baseUri);
});

builder.Services.AddTransient<IAdminDAL, AdminDAL>();
builder.Services.AddTransient<IAddressDAL, AddressDAL>();
builder.Services.AddTransient<IPeopleDAL, PeopleDAL>();
builder.Services.AddTransient<IContactDAL, ContactDAL>();
builder.Services.AddTransient<ICustomerDAL, CustomerDAL>();
builder.Services.AddTransient<IProfessionalDAL, ProfessionalDAL>();
builder.Services.AddTransient<ICompanyDAL, CompanyDAL>();
builder.Services.AddTransient<IScheduleDAL, ScheduleDAL>();
builder.Services.AddTransient<IUserDAL, UserDAL>();
builder.Services.AddTransient<ISettingMailDAL, SettingMailDAL>();
builder.Services.AddTransient<IMailConfigurationDAL, MailConfigurationDAL>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
