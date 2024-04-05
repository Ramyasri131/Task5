using EmployeeDirectory.MenuHandler;
using EmployeeDirectory.ConstantData;
namespace EmployeeDirectory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Constant.GetRoles();
            MenuManager displayOptions = new();
            displayOptions.DisplayMainMenu();
        }
    }
}