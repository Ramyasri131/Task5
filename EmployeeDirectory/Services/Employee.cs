using EmployeeDirectory.Utilities;
using EmployeeDirectory.DLL.StaticData;
using EmployeeDirectory.BAL.Validators;
using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.BAL.Providers;

namespace EmployeeDirectory.Services
{
    public interface IEmployeeService
    {
        public void GetEmployeeInput();
        public void DisplayEmployees();
        public void DisplayEmployee();
        public void EditEmployee();
        public void DeleteEmployee();
    }

    public class Employee:IEmployeeService
    {
        public void GetEmployeeInput()
        {
            Helpers.Print("Enter First Name:");
            string? firstName = Console.ReadLine();
            Helpers.Print("Enter Last Name:");
            string? lastName = Console.ReadLine();
            Helpers.Print("Enter Date Of Birth in (dd/mm/yyyy):");
            string? dateOfBirth = Console.ReadLine();
            Helpers.Print("Enter Email:");
            string? email = Console.ReadLine();
            Helpers.Print("Enter Mobile Number:");
            string? mobileNumber = Console.ReadLine();
            Helpers.Print("Enter Date Of Join in (dd/mm/yyyy):");
            string? dateOfJoin = Console.ReadLine();
            Helpers.Print("select Location:");
            string location = SelectValidDetails("location", Constant.Locations);
            Helpers.Print("select JobTitle:");
            Constant.GetRoles();
            string jobTitle = SelectValidDetails("jobTitle", Constant.Roles);
            Helpers.Print("select Department:");
            string department = SelectValidDetails("department", Constant.Departments);
            Helpers.Print("select Manager");
            string manager = SelectValidDetails("Manager", Constant.Managers);
            Helpers.Print("select Project");
            string project = SelectValidDetails("Project", Constant.Projects);
            DLL.Models.Employee employeeInput = new()
            {
                Id = "0000",
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Email = email,
                MobileNumber = mobileNumber,
                DateOfJoin = dateOfJoin,
                Location = location,
                JobTitle = jobTitle,
                Department = department,
                Manager = manager,
                Project = project
            };
            List<string> InvalidInputs = EmployeeValidator.GetInValidInputs(employeeInput);
            if (InvalidInputs.Count == 0)
            {
               BAL.Providers.Employee.AddEmployee(employeeInput);
            }
            else
            {
                foreach (string input in InvalidInputs)
                {
                    Helpers.Print($"Enter Valid {input}");
                }
                GetEmployeeInput();
            }
        }

        public void DisplayEmployees()
        {
            try
            {
                List<DLL.Models.Employee> employeeData = BAL.Providers.Employee.GetEmployees();
                DisplayHelper.PrintEmployeesData(employeeData);
            }
            catch (EmptyDataBase)
            {
                throw;
            }
        }

        public void DisplayEmployee()
        {
            Helpers.Print("Enter Employee Id");
            string? id = Console.ReadLine();
            try
            {
                DLL.Models.Employee employeeData = BAL.Providers.Employee.GetEmployee(id);
                DisplayHelper.PrintEmployeeData(employeeData);
            }
            catch (EmptyDataBase)
            {
                throw;
            }
            catch (InvalidEmployeeId)
            {
                throw;
            }
        }

        public void EditEmployee()
        {
            Helpers.Print("Enter Employee Id");
            string? id = Console.ReadLine();
            id = id!.ToUpper();
            Helpers.Print("Field to edit");
            foreach (KeyValuePair<int, string> item in Constant.EmployeeDataLabels)
            {
                Helpers.Print(item.Key + " " + item.Value);
            }
            Helpers.Print("Enter Option");
            int selectedOption;
            try
            {
                selectedOption = int.Parse(Console.ReadLine()!);
            }
            catch (FormatException)
            {
                throw;
            }
            string label = Constant.EmployeeDataLabels[selectedOption];
            string? selectedData;
            if (string.Equals(label, "location"))
            {
                selectedData = SelectValidDetails(label, Constant.Locations);
            }
            else if (string.Equals(label, "department"))
            {
                selectedData = SelectValidDetails(label, Constant.Departments);
            }
            else if (string.Equals(label, "JobTitle"))
            {
                Constant.GetRoles();
                selectedData = SelectValidDetails(label, Constant.Roles);
            }
            else if (string.Equals(label, "manager"))
            {
                selectedData = SelectValidDetails(label, Constant.Managers);
            }
            else if (string.Equals(label, "project"))
            {
                selectedData = SelectValidDetails(label, Constant.Projects);
            }
            else
            {
                try
                {
                    selectedData = GetValidDetails(label);
                }
                catch (InvalidInput)
                {
                    throw;
                }
            }
            try
            {
                BAL.Providers.Employee.EditEmployeeDetails(selectedData, id, selectedOption);
            }
            catch (InvalidEmployeeId)
            {
                throw;
            }
        }

        public static string SelectValidDetails(string label, Dictionary<int, string> list)
        {
            foreach (KeyValuePair<int, string> item in list)
            {
                Helpers.Print(item.Key + " " + item.Value);
            }
            string? enteredKey = Console.ReadLine();
            int selectedKey = int.Parse(enteredKey!);
            if (selectedKey <= 0 && selectedKey >= list.Count)
            {
                Helpers.Print("Choose from the given options");
                return SelectValidDetails(label, list);
            }
            else
            {
                return list[selectedKey];
            }
        }

        public static string GetValidDetails(string label)
        {
            Helpers.Print($"Enter {label}");
            string? inputData = Console.ReadLine();
            try
            {
                string employeeData=EmployeeValidator.ValidateData(label,inputData);
                return inputData!;
            }
            catch (InvalidInput)
            {
                throw;
            }
        }

        public void DeleteEmployee()
        {
            Helpers.Print("Enter Employee Id To Delete");
            string? enteredEmpId = Console.ReadLine();
            try
            {
                BAL.Providers.Employee.DeleteEmployee(enteredEmpId);
            }
            catch (EmptyDataBase)
            {
                throw;
            }
            catch (InvalidEmployeeId)
            {
                throw;
            }
        }
    }
}