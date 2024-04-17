using EmployeeDirectory.Utilities;
using EmployeeDirectory.DLL.StaticData;
using EmployeeDirectory.Services;

//to display the options
namespace EmployeeDirectory.Manager
{
    public class Menu
    {
        public void DisplayMainMenu()
        {
            Helpers.Print("Main Menu");
            foreach (var item in Constant.MainMenu)
            {
                Helpers.Print(item.Key, item.Value);
            }
            Helpers.Print("");
            Helpers.Print("Enter Your Choice:");
            string? enteredOption = Console.ReadLine();
            try
            {
               int selectedOption = int.Parse(enteredOption!);
                switch (selectedOption)
                {
                    case 1:
                        DisplayEmployeeManagementMenu();
                        break;
                    case 2:
                        DisplayRoleManagementMenu();
                        break;
                    case 3:
                        Helpers.Print("Exit");
                        return;
                    default:
                        Helpers.Print("Invalid Option");
                        break;
                }
            }
            catch (FormatException e)
            {
               Helpers.Print(e.ToString());
               DisplayMainMenu();
            }
        }
        public void DisplayEmployeeManagementMenu()
        {
            Helpers.Print("Employee Management");
            foreach (var item in Constant.EmployeMaangementMenu)
            {
                Helpers.Print(item.Key, item.Value);
            }
            Helpers.Print("");
            Helpers.Print("Enter your choice:");
            Employee employeeInputCollector = new();
            string? enteredValue = Console.ReadLine();
            try
            {
                int selectedOption = int.Parse(enteredValue!);
                switch (selectedOption)
                {
                    case 1:
                        employeeInputCollector.GetEmployeeInput();
                        break;
                    case 2:
                        employeeInputCollector.DisplayEmployees();
                        break;
                    case 3:
                        employeeInputCollector.DisplayEmployee();
                        break;
                    case 4:
                        employeeInputCollector.EditEmployee();
                        break;
                    case 5:
                        employeeInputCollector.DeleteEmployee();
                        break;
                    case 6:
                        DisplayMainMenu();
                        return;
                    default:
                        Helpers.Print("Enter valid option");
                        break;
                }
            }
            catch (FormatException e)
            {
                Helpers.Print(e.ToString());
            }
            DisplayEmployeeManagementMenu();
        }
        public void DisplayRoleManagementMenu()
        {
            Helpers.Print("Role Management");
            foreach (var item in Constant.RoleManagementMenu)
            {
                Helpers.Print(item.Key, item.Value);
            }
            Helpers.Print("");
            Helpers.Print("Enter your choice:");
            Role roleDetailsCollector = new();
            string? enteredValue = Console.ReadLine();
            try
            {
                int selectedOption = int.Parse(enteredValue!);
                switch (selectedOption)
                {
                    case 1:
                        roleDetailsCollector.CollectRoleDetails();
                        break;
                    case 2:
                        roleDetailsCollector.DisplayRoles();
                        break;
                    case 3:
                        DisplayMainMenu();
                        break;
                    default:
                        Helpers.Print("Invalid Option");
                        break;
                }
            }
            catch (FormatException e)
            {
                Helpers.Print(e.ToString());
            }
            DisplayRoleManagementMenu();
        }
    }
}