using EmployeeDirectory.Utilities;
using EmployeeDirectory.DLL.StaticData;
using EmployeeDirectory.Services;

namespace EmployeeDirectory.Manager
{
    public interface IMenuManager
    {
        public void DisplayMainMenu();
        public void DisplayEmployeeManagementMenu();
        public void DisplayRoleManagementMenu();
    }

    public class Menu:IMenuManager
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRoleService _roleService;

        public Menu(IEmployeeService employeeService, IRoleService roleService)
        {
            _employeeService = employeeService;
            _roleService = roleService;
        }
       
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
            string? enteredValue = Console.ReadLine();
            try
            {
                int selectedOption = int.Parse(enteredValue!);
                switch (selectedOption)
                {
                    case 1:
                        _employeeService.GetEmployeeInput();
                        break;
                    case 2:
                        _employeeService.DisplayEmployees();
                        break;
                    case 3:
                        _employeeService.DisplayEmployee();
                        break;
                    case 4:
                        _employeeService.EditEmployee();
                        break;
                    case 5:
                        _employeeService.DeleteEmployee();
                        break;
                    case 6:
                        DisplayMainMenu();
                        return;
                    default:
                        Helpers.Print("Enter valid option");
                        break;
                }
            }
            catch (Exception e)
            {
                Helpers.Print(e.ToString());
                DisplayEmployeeManagementMenu();
            }
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
            string? enteredValue = Console.ReadLine();
            try
            {
                int selectedOption = int.Parse(enteredValue!);
                switch (selectedOption)
                {
                    case 1:
                        _roleService.CollectRoleDetails();
                        break;
                    case 2:
                        _roleService.DisplayRoles();
                        break;
                    case 3:
                        DisplayMainMenu();
                        break;
                    default:
                        Helpers.Print("Invalid Option");
                        break;
                }
            }
            catch (Exception e)
            {
                Helpers.Print(e.ToString());
                DisplayRoleManagementMenu();
            }
        }
    }
}