using CManager.Core.Application.Interfaces;
using CManager.Core.Application.Services;
using CManager.Core.Infrastructure.Repos;
using CManager.Core.Presentation.ConsoleApp.Controllers;
using Microsoft.Extensions.DependencyInjection;

//delen som kopplar ihop alla delar för att staarta appen
var services = new ServiceCollection()
    .AddScoped<ICustomerRepo, CustomerRepo>()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<MenuController>()
    .BuildServiceProvider();

var menu = services.GetRequiredService<MenuController>();
menu.ShowMenu();
