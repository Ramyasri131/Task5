using EmployeeDirectory.Manager;
using EmployeeDirectory.Services;
using Microsoft.Extensions.DependencyInjection;
namespace EmployeeDirectory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IEmployeeService, Services.Employee>();
            serviceCollection.AddSingleton<IRoleService, Services.Role>();
            serviceCollection.AddSingleton<IMenuManager, Menu>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            IMenuManager displayOptions = serviceProvider.GetService<IMenuManager>()!;
            displayOptions.DisplayMainMenu();
        }
    }
}