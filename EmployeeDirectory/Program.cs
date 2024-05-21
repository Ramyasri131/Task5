using EmployeeDirectory.Manager;
using EmployeeDirectory.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace EmployeeDirectory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IEmployeeService, Services.Employee>();
            serviceCollection.AddSingleton<IRoleService, Services.Role>();
            serviceCollection.AddSingleton<IMenuManager, Menu>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IMenuManager displayOptions = serviceProvider.GetService<IMenuManager>()!;
            displayOptions.DisplayMainMenu();
        }
    }
}