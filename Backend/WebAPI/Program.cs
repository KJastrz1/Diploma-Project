using Microsoft.EntityFrameworkCore;
using Shared.Mappers;
using Backend.Data;
using Backend.Services;
using System.Text.Json.Serialization;
using Backend.Utils;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Mappers));

builder.Services.AddDbContext<ClinicDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<TimeSpanSchemaFilter>();
});

builder.Services.AddScoped<IPatientsService, PatientsService>();
builder.Services.AddScoped<IDoctorsService, DoctorsService>();
builder.Services.AddScoped<IClinicsService, ClinicsService>();
builder.Services.AddScoped<IDoctorSchedulesService, DoctorSchedulesService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
