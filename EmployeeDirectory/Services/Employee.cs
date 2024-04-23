using EmployeeDirectory.Utilities;
using EmployeeDirectory.DAL.StaticData;
using EmployeeDirectory.BAL.Validators;
using EmployeeDirectory.DAL.Exceptions;
using EmployeeDirectory.Interfaces;

namespace EmployeeDirectory.Services
{
    public class Employee: IEmployeeService
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
            long mobileNumber = long.Parse(Console.ReadLine()!);
            Helpers.Print("Enter Date Of Join in (dd/mm/yyyy):");
            string? dateOfJoin = Console.ReadLine();
            Helpers.Print("select Location:");
            string location = SelectValidDetails("location", Constant.Locations);
            Helpers.Print("select JobTitle:");
            string jobTitle = SelectValidDetails("jobTitle", Constant.Roles);
            Helpers.Print("select Department:");
            string department = SelectValidDetails("department", Constant.Departments);
            Helpers.Print("select Manager");
            string manager = SelectValidDetails("Manager", Constant.Managers);
            Helpers.Print("select Project");
            string project = SelectValidDetails("Project", Constant.Projects);
            BAL.DTO.Employee employeeInput = new()
            {
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
            List<string> InvalidInputs = EmployeeValidator.ValidateDetails(employeeInput);
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
                List<DAL.Models.Employee> employeeData = BAL.Providers.Employee.GetEmployees();
                DisplayHelper.PrintEmployeesData(employeeData);
            }
            catch (RecordNotFound)
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
                DAL.Models.Employee employeeData = BAL.Providers.Employee.GetEmployeeById(id);
                DisplayHelper.PrintEmployeeData(employeeData);
            }
            catch (RecordNotFound)
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
            if (string.Equals(label, "Location"))
            {
                selectedData = SelectValidDetails(label, Constant.Locations);
            }
            else if (string.Equals(label, "Department"))
            {
                selectedData = SelectValidDetails(label, Constant.Departments);
            }
            else if (string.Equals(label, "JobTitle"))
            {
                selectedData = SelectValidDetails(label, Constant.Roles);
            }
            else if (string.Equals(label, "Manager"))
            {
                selectedData = SelectValidDetails(label, Constant.Managers);
            }
            else if (string.Equals(label, "Project"))
            {
                selectedData = SelectValidDetails(label, Constant.Projects);
            }
            else
            {
                try
                {
                    selectedData = GetValidDetails(label);
                }
                catch (BAL.Exceptions.InvalidData)
                {
                    throw;
                }
            }
            try
            {
                BAL.Providers.Employee.EditEmployeeDetails(selectedData, id, selectedOption);
            }
            catch (RecordNotFound)
            {
                throw;
            }
            catch (FormatException)
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
            if (selectedKey <= 0 && selectedKey > list.Count)
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
                EmployeeValidator.ValidateData(label,inputData!);
                return inputData!;
            }
            catch (BAL.Exceptions.InvalidData)
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
            catch (RecordNotFound)
            {
                throw;
            }
            catch (BAL.Exceptions.InvalidData)
            {
                throw;
            }
        }
    }
}