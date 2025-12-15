using CManager.Application.Services;
using CManager.Infrastructure.Repos;
using CManager.Presentation.ConsoleApp.Controllers;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddScoped<ICustomerService, CustomerService>()  //service
    .AddScoped<ICustomerRepo, CustomerRepo>()        //repository
    .AddScoped<MenuController>()                     //meny
    .BuildServiceProvider();

//startar menyn
var controller = services.GetRequiredService<MenuController>();
controller.ShowMenu();
