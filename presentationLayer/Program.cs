using EmployeeDirectory.MenuHandler;
namespace EmployeeDirectory
{

    internal class Program
    {
        static void Main(string[] args)
        {
            MenuManager displayOptions = new MenuManager();
            displayOptions.DisplayMainMenu();
        }

    }
}
