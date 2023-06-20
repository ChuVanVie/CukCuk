using Microsoft.AspNetCore.Mvc;
using Misa.Cukcuk.Repositories;
using Misa.Cukcuk.Repositories.BaseRepository;
using Misa.Cukcuk.Repositories.CalculationUnitRepository;
using Misa.Cukcuk.Repositories.MaterialGroupRepositoty;
using Misa.Cukcuk.Repositories.MaterialRepository;
using Misa.Cukcuk.Repositories.UnitConvertionRepository;
using Misa.Cukcuk.Repositories.WarehouseRepository;
using Misa.CukCuk.Materials.Services.BaseService;
using Misa.CukCuk.Materials.Services.CalculationUnitService;
using Misa.CukCuk.Materials.Services.MaterialGroupService;
using Misa.CukCuk.Materials.Services.MaterialService;
using Misa.CukCuk.Materials.Services.WarehouseService;
using Misa.CukCuk.Services.Services.UnitConvertionService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers();
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    })
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency injection
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();

builder.Services.AddScoped<IMaterialGroupService, MaterialGroupService>();
builder.Services.AddScoped<IMaterialGroupRepository, MaterialGroupRepository>();

builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

builder.Services.AddScoped<ICalculationUnitService, CalculationUnitService>();
builder.Services.AddScoped<ICalculationUnitRepository, CalculationUnitRepository>();

builder.Services.AddScoped<IUnitConvertionService, UnitConvertionService>();
builder.Services.AddScoped<IUnitConvertionRepository, UnitConvertionRepository>();


DatabaseConnect.connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
