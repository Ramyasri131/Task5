using EmployeeDirectory.Utilities;
using EmployeeDirectory.DAL.StaticData;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.DAL.Exceptions;
using EmployeeDirectory.BAL.Exceptions;
using System.Text.Json;

namespace EmployeeDirectory.Manager
{
   
    public class Menu(IEmployeeService employeeService, IRoleService roleService) : IMenuManager
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IRoleService _roleService = roleService;

        public void DisplayMainMenu()
        {
            Constants.GetRoles();
            Display.Print("Main Menu");
            foreach (var item in Constants.MainMenu)
            {
                Display.Print(item.Key, item.Value);
            }
            Display.Print("");
            Display.Print("Enter Your Choice:");
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
                        Display.Print("Exit");
                        return;
                    default:
                        Display.Print("Invalid Option");
                        break;
                }
            }
            catch (FormatException e)
            {
                Display.Print(e.ToString());
                DisplayMainMenu();
            }
        }

        public void DisplayEmployeeManagementMenu()
        {
            Display.Print("Employee Management");
            foreach (var item in Constants.EmployeeManagementMenu)
            {
                Display.Print(item.Key, item.Value);
            }
            Display.Print("");
            Display.Print("Enter your choice:");
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
                        Display.Print("Enter valid option");
                        break;
                }
            }
            catch (FormatException e)
            {
                Display.Print(e.ToString());
            }
            catch(RecordNotFound ex)
            {
                Display.Print(ex.ToString());

            }
            catch (InvalidData ex)
            {
                Display.Print(ex.ToString());

            }
            catch (JsonException ex)
            {
                Display.Print(ex.ToString());
            }
            finally
            {
                DisplayEmployeeManagementMenu();
            }
        }

        public void DisplayRoleManagementMenu()
        {
            Display.Print("Role Management");
            foreach (var item in Constants.RoleManagementMenu)
            {
                Display.Print(item.Key, item.Value);
            }
            Display.Print("");
            Display.Print("Enter your choice:");
            string? enteredValue = Console.ReadLine();
            try
            {
                int selectedOption = int.Parse(enteredValue!);
                switch (selectedOption)
                {
                    case 1:
                        _roleService.GetRoles();
                        break;
                    case 2:
                        _roleService.DisplayRoles();
                        break;
                    case 3:
                        DisplayMainMenu();
                        break;
                    default:
                        Display.Print("Invalid Option");
                        break;
                }
            }
            catch (FormatException e)
            {
                Display.Print(e.ToString());
            }
            catch (RecordNotFound ex)
            {
                Display.Print(ex.ToString());
            }
            catch (InvalidData ex)
            {
                Display.Print(ex.ToString());
            }
            catch(JsonException ex)
            {
                Display.Print(ex.ToString());
            }
            finally
            {
                DisplayRoleManagementMenu();
            }
        }
    }
}