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
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
    options => builder.Configuration.Bind("JwtSettings", options));

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
    options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        ReferenceHandler = ReferenceHandler.Preserve,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    }));
}).AddNewtonsoftJson();

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

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
