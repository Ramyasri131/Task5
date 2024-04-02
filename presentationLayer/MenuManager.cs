using EmployeeDirectory.Constant;
using EmployeeDirectory.UserActions;
using EmployeeDirectory.Printing;
using EmployeeDirectory.Utilities;

//to display the options
namespace EmployeeDirectory.MenuHandler
{
    public class MenuManager
    {
        Helpers helper = new Helpers();
        DataPrinter dataPrinter = new DataPrinter();
        public void DisplayMainMenu()
        {
            helper.Print("Main Menu");
            foreach (var item in MenuList.MainMenu)
            {
                helper.Print(item.Key, item.Value);
            }
            bool continueRunning = true;
            while (continueRunning)
            {
                helper.Print("");
                helper.Print("Enter Your Choice:");
                string enteredOption = Console.ReadLine();
                if (int.TryParse(enteredOption, out _))
                {
                    int selectedOption=int.Parse(enteredOption);
                    switch (selectedOption)
                    {
                        case 1:
                            DisplayEmployeeManagementMenu();
                            break;
                        case 2:
                            DisplayRoleManagementMenu();
                            break;
                        case 3:
                            continueRunning = false;
                            helper.Print("Exit");
                            break;
                        default:
                            helper.Print("Invalid Option");
                            break;

                    }
                }
                else
                {
                    helper.Print("Enter Integer");
                }
            }
        }
        public void DisplayEmployeeManagementMenu()
        {
            helper.Print("Employee Management");
            foreach (var item in MenuList.EmployeMaangementMenu)
            {
                helper.Print(item.Key, item.Value);
            }
            helper.Print("");
            helper.Print("Enter your choice:");
            UserActionHandler selectOptions = new UserActionHandler();
            string enteredValue = Console.ReadLine();
            if (int.TryParse(enteredValue, out _))
            {
                int selectedTask = int.Parse(enteredValue);
                
                switch (selectedTask)
                {
                    case 1:
                        selectOptions.OnSelectAddEmployee();
                        DisplayEmployeeManagementMenu();
                        break;
                    case 2:
                        List<EmployeeData>employeeData=selectOptions.OnDisplayAll();
                        dataPrinter.PrintEmployeeData(employeeData);
                        DisplayEmployeeManagementMenu();
                        break;
                    case 3:
                        EmployeeData employee=selectOptions.OnSelectDisplayOne();
                        dataPrinter.PrintEmployeeData(employee);
                        DisplayEmployeeManagementMenu();
                        break;
                    case 4:
                        selectOptions.OnSelectEditEmployee();
                        DisplayEmployeeManagementMenu();
                        break;
                    case 5:
                        selectOptions.OnSelectDeleteEmployee();
                        DisplayEmployeeManagementMenu();
                        break;
                    case 6:
                        DisplayMainMenu();
                        break;
                    default:
                        helper.Print("Enter valid option");
                        break;
                }
            }
            else
            {
                helper.Print("Enter Integer");
            }
        }
        public void DisplayRoleManagementMenu()
        {
            helper.Print("Role Management");
            foreach (var item in MenuList.RoleManagementMenu)
            {
                helper.Print(item.Key, item.Value);
            }
            helper.Print("");
            helper.Print("Enter your choice:");
            UserActionHandler selectOptions = new UserActionHandler();
            string enteredValue = Console.ReadLine();
            if (int.TryParse(enteredValue, out _))
            {
                int selectedTask = int.Parse(enteredValue);
                switch (selectedTask)
                {
                    case 1:
                        selectOptions.OnSelectAddRole();
                        DisplayRoleManagementMenu();
                        break;
                    case 2:
                        List<RoleData>roleData=selectOptions.OnSelectDisplayAllRole();
                        dataPrinter.PrinRoleData(roleData);

                        DisplayRoleManagementMenu();
                        break;
                    case 3:
                        DisplayMainMenu();
                        break;
                    default:
                        helper.Print("Invalid Option");
                        break;
                }
            }
            else
            {
                helper.Print("Enter Integer");
            }
        }
    }
    
}
