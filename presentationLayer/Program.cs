using EmployeeDirectory.DLL.StaticData;
using EmployeeDirectory.Manager;
namespace EmployeeDirectory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Constant.GetRoles();
            Menu displayOptions = new();
            displayOptions.DisplayMainMenu();
        }
    }
}