using EmployeeDirectory.Utilities;
using EmployeeDirectory.DAL.StaticData;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.DAL.Exceptions;
using EmployeeDirectory.BAL.Exceptions;
using System.Text.Json;

namespace EmployeeDirectory.Manager
{
   
    public class Menu : IMenuManager
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
            Constant.GetRoles();
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
            foreach (var item in Constant.EmployeeManagementMenu)
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
            catch (FormatException e)
            {
                Helpers.Print(e.ToString());
            }
            catch(RecordNotFound ex)
            {
                Helpers.Print(ex.ToString());

            }
            catch (InvalidData ex)
            {
                Helpers.Print(ex.ToString());

            }
            catch (JsonException ex)
            {
                Helpers.Print(ex.ToString());
            }
            finally
            {
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
            catch (FormatException e)
            {
                Helpers.Print(e.ToString());
            }
            catch (RecordNotFound ex)
            {
                Helpers.Print(ex.ToString());
            }
            catch (InvalidData ex)
            {
                Helpers.Print(ex.ToString());
            }
            catch(JsonException ex)
            {
                Helpers.Print(ex.ToString());
            }
            finally
            {
                DisplayRoleManagementMenu();
            }
        }
    }
}