using EmployeeDirectory.Utilities;
using EmployeeDirectory.DAL.StaticData;
using EmployeeDirectory.BAL.Validators;
using EmployeeDirectory.DAL.Exceptions;
using EmployeeDirectory.Interfaces;

namespace EmployeeDirectory.Services
{
    public class EmployeeService: IEmployeeService
    {
        public void GetEmployeeInput()
        {
            Display.Print("Enter First Name:");
            string? firstName = Console.ReadLine();
            Display.Print("Enter Last Name:");
            string? lastName = Console.ReadLine();
            Display.Print("Enter Date Of Birth in (dd/mm/yyyy):");
            string? dateOfBirth = Console.ReadLine();
            Display.Print("Enter Email:");
            string? email = Console.ReadLine();
            Display.Print("Enter Mobile Number:");
            long mobileNumber = long.Parse(Console.ReadLine()!);
            Display.Print("Enter Date Of Join in (dd/mm/yyyy):");
            string? dateOfJoin = Console.ReadLine();
            Display.Print("select Location:");
            string location = SelectValidDetails("location", Constants.Locations);
            Display.Print("select JobTitle:");
            string jobTitle = SelectValidDetails("jobTitle", Constants.Roles);
            Display.Print("select Department:");
            string department = SelectValidDetails("department", Constants.Departments);
            Display.Print("select Manager");
            string manager = SelectValidDetails("Manager", Constants.Managers);
            Display.Print("select Project");
            string project = SelectValidDetails("Project", Constants.Projects);
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
               BAL.Providers.EmployeeProvider.AddEmployee(employeeInput);
            }
            else
            {
                foreach (string input in InvalidInputs)
                {
                    Display.Print($"Enter Valid {input}");
                }
                GetEmployeeInput();
            }
        }

        public void DisplayEmployees()
        {
            try
            {
                List<DAL.Models.Employee> employeeData = BAL.Providers.EmployeeProvider.GetEmployees();
                Display.PrintEmployeesData(employeeData);
            }
            catch (RecordNotFound)
            {
                throw;
            }
        }

        public void DisplayEmployee()
        {
            Display.Print("Enter Employee Id");
            string? id = Console.ReadLine();
            try
            {
                DAL.Models.Employee employeeData = BAL.Providers.EmployeeProvider.GetEmployeeById(id);
                Display.PrintEmployeeData(employeeData);
            }
            catch (RecordNotFound)
            {
                throw;
            }
        }

        public void EditEmployee()
        {
            Display.Print("Enter Employee Id");
            string? id = Console.ReadLine();
            id = id!.ToUpper();
            Display.Print("Field to edit");
            foreach (KeyValuePair<int, string> item in Constants.EmployeeDataLabels)
            {
                Display.Print(item.Key + " " + item.Value);
            }
            Display.Print("Enter Option");
            int selectedOption;
            try
            {
                selectedOption = int.Parse(Console.ReadLine()!);
                string label = Constants.EmployeeDataLabels[selectedOption];
                string? selectedData;
                if (string.Equals(label, "Location"))
                {
                    selectedData = SelectValidDetails(label, Constants.Locations);
                }
                else if (string.Equals(label, "Department"))
                {
                    selectedData = SelectValidDetails(label, Constants.Departments);
                }
                else if (string.Equals(label, "JobTitle"))
                {
                    selectedData = SelectValidDetails(label, Constants.Roles);
                }
                else if (string.Equals(label, "Manager"))
                {
                    selectedData = SelectValidDetails(label, Constants.Managers);
                }
                else if (string.Equals(label, "Project"))
                {
                    selectedData = SelectValidDetails(label, Constants.Projects);
                }
                else
                {
                    selectedData = GetValidDetails(label);
                }
                 BAL.Providers.EmployeeProvider.EditEmployeeDetails(selectedData, id, selectedOption);
            }
            catch (BAL.Exceptions.InvalidData)
            {
                throw;
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
                Display.Print(item.Key + " " + item.Value);
            }
            string? enteredKey = Console.ReadLine();
            int selectedKey = int.Parse(enteredKey!);
            if (selectedKey <= 0 && selectedKey > list.Count)
            {
                Display.Print("Choose from the given options");
                return SelectValidDetails(label, list);
            }
            else
            {
                return list[selectedKey];
            }
        }

        public static string GetValidDetails(string label)
        {
            Display.Print($"Enter {label}");
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
            Display.Print("Enter Employee Id To Delete");
            string? enteredEmpId = Console.ReadLine();
            try
            {
                BAL.Providers.EmployeeProvider.DeleteEmployee(enteredEmpId);
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