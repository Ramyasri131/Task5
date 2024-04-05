using EmployeeDirectory.ConstantData;
using EmployeeDirectory.Utilities;
using EmployeeDirectory.InputHandling;
using EmployeeDirectory.RoleDetails;

//to display the options
namespace EmployeeDirectory.MenuHandler
{
    public class MenuManager
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
            if (int.TryParse(enteredOption, out _))
            {
                int selectedOption = int.Parse(enteredOption);
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
            else
            {
                Helpers.Print("Enter Integer");
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
            EmployeeServices employeeInputCollector = new();
            string? enteredValue = Console.ReadLine();
            if (int.TryParse(enteredValue, out _))
            {
                int selectedTask = int.Parse(enteredValue);
                switch (selectedTask)
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
            else
            {
                Helpers.Print("Enter Integer");
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
            RoleServices roleDetailsCollector = new();
            string? enteredValue = Console.ReadLine();
            if (int.TryParse(enteredValue, out _))
            {
                int selectedTask = int.Parse(enteredValue);
                switch (selectedTask)
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
            else
            {
                Helpers.Print("Enter Integer");
            }
            DisplayRoleManagementMenu();
        }
    }
}