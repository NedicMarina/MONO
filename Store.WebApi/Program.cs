using Store.Repository;
using Store.Repository.Common;
using Store.Service;
using Store.Service.Common;
using Autofac;
using Autofac.Extensions.DependencyInjection;   

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<AnimalRepository>()
        .As<IAnimalRepository>()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterType<FoodRepository>()
        .As<IFoodRepository>()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterType<AnimalService>()
        .As<IAnimalService>()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterType<FoodService>()
        .As<IFoodService>()
        .InstancePerLifetimeScope();
});

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddScoped<IAnimalService, AnimalService>();
//builder.Services.AddScoped<IFoodService, FoodService>();

//builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
//builder.Services.AddScoped<IFoodRepository, FoodRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

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
