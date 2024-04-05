using EmployeeDirectory.EmployeeData;
using EmployeeDirectory.Validation;
using EmployeeDirectory.ConstantData;
using EmployeeDirectory.EmployeeManagement;
using EmployeeDirectory.Printing;
using EmployeeDirectory.Utilities;
using System.Text.Json;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EmployeeDirectory.InputHandling
{
    public class EmployeeServices
    {
        private readonly DisplayHelper _printer = new();
        private readonly EmployeeManagementSystem _employeeManagementSystem = new();
        private readonly string _employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\Employee.json");
        public void GetEmployeeInput()
        {
            List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(_employeeJsonData)!;
            int noOfEmployees = employees.Count + 1;
            string EmpId = string.Format("{0:0000}", noOfEmployees);
            EmpId = "TZ" + EmpId;
            Helpers.Print("Enter First Name:");
            string? FirstName = Console.ReadLine();
            Helpers.Print("Enter Last Name:");
            string? LastName = Console.ReadLine();
            Helpers.Print("Enter Date Of Birth in dd/mm/yyyy or mm/dd/yyyy format:");
            string? DateOfBirth = Console.ReadLine();
            Helpers.Print("Enter Email in the format user@tezo.com:");
            string? Email = Console.ReadLine();
            Helpers.Print("Enter Mobile Number:");
            string? MobileNumber = Console.ReadLine();
            Helpers.Print("Enter Date Of Birth in dd / mm / yyyy or mm//dd/yyyy format:");
            string? DateOfJoin = Console.ReadLine();
            Helpers.Print("select Location:");
            string Location = SelectValidDetails("location", Constant.Locations);
            Helpers.Print("select JobTitle:");
            string JobTitle = SelectValidDetails("jobTitle", Constant.Roles);
            Helpers.Print("select Department:");
            string Department = SelectValidDetails("department", Constant.Departments);
            Helpers.Print("select Manager");
            string Manager = SelectValidDetails("Manager", Constant.Managers);
            Helpers.Print("select Project");
            string Project = SelectValidDetails("Project", Constant.Projects);
            Employee employeeInput = new()
            {
                Id = EmpId,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                Email = Email,
                MobileNumber = MobileNumber,
                DateOfJoin = DateOfJoin,
                Location = Location,
                JobTitle = JobTitle,
                Department = Department,
                Manager = Manager,
                Project = Project
            };
            InputValidator validator = new();
            List<string> InvalidInputs = validator.IsValidEmployee(employeeInput);
            if (InvalidInputs.Count == 0)
            {
                _employeeManagementSystem.AddEmployee(employeeInput);
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
            if (!string.IsNullOrEmpty(_employeeJsonData))
            {
                List<Employee> employeeData = _employeeManagementSystem.GetAllEmployees(_employeeJsonData);
                _printer.PrintEmployeesData(employeeData);
            }
            else
            {
                Helpers.Print("Database is empty");
            }
        }

        public void DisplayEmployee()
        {
            if (!string.IsNullOrEmpty(_employeeJsonData))
            {
                Helpers.Print("Enter Employee Id");
                string? enteredEmpId = Console.ReadLine();
                enteredEmpId = enteredEmpId?.ToUpper();
                List<Employee> employeeData = JsonSerializer.Deserialize<List<Employee>>(_employeeJsonData)!;
                var employee = employeeData.Single(e => e.Id == enteredEmpId);
                if (employee is not null)
                {
                    _printer.PrintEmployeeData(employee);
                }
                else
                {
                    Helpers.Print("Entered EmpId not in list");
                }
            }
            else
            {
                Helpers.Print("Database is empty");
            }

        }
        public void EditEmployee()
        {
            if (string.IsNullOrEmpty(_employeeJsonData))
            {
                Helpers.Print("DataBase is Empty");
            }
            else
            {
                Helpers.Print("Enter Employee Id To Edit");
                string? enteredEmpId = Console.ReadLine();
                if (!string.IsNullOrEmpty(enteredEmpId))
                {
                    enteredEmpId = enteredEmpId?.ToUpper();
                    List<Employee> employeeData = JsonSerializer.Deserialize<List<Employee>>(_employeeJsonData)!;
                    Employee? employee = employeeData.Single(e => e.Id == enteredEmpId);
                    if (employee is not null)
                    {
                        Helpers.Print("Field to edit");
                        foreach (KeyValuePair<int, string> item in Constant.EmployeeDataLabels)
                        {
                            Helpers.Print(item.Key + " " + item.Value);
                        }
                        Helpers.Print("Enter Option");
                        string? EnteredOption = Console.ReadLine();
                        int selectedOption;
                        if (int.TryParse(EnteredOption, out _))
                        {
                            selectedOption = int.Parse(EnteredOption);
                            string label = Constant.EmployeeDataLabels[selectedOption];
                            string? dataToEdit;
                            if (string.Equals(label, "location"))
                            {
                                dataToEdit = SelectValidDetails(label, Constant.Locations);
                            }
                            else if (string.Equals(label, "department"))
                            {
                                dataToEdit = SelectValidDetails(label, Constant.Departments);
                            }
                            else if (string.Equals(label, "JobTitle"))
                            {

                                dataToEdit = SelectValidDetails(label, Constant.Roles);
                            }
                            else if (string.Equals(label, "manager"))
                            {
                                dataToEdit = SelectValidDetails(label, Constant.Managers);
                            }
                            else if (string.Equals(label, "project"))
                            {
                                dataToEdit = SelectValidDetails(label, Constant.Projects);
                            }
                            else
                            {
                                dataToEdit = GetValidDetails(label);
                            }
                            _employeeManagementSystem.EditEmployeeDetails(dataToEdit, _employeeJsonData, enteredEmpId, selectedOption);
                        }
                        else
                        {
                            Helpers.Print("Entere valid option");
                        }
                    }
                    else
                    {
                        Helpers.Print("Enter employee not in list");
                    }
                }
                else
                {
                    EditEmployee();
                }
            }
        }
        public string SelectValidDetails(string label, Dictionary<int, string> list)
        {
            foreach (KeyValuePair<int, string> item in list)
            {
                Helpers.Print(item.Key + " " + item.Value);
            }
            string? enteredKey = Console.ReadLine();
            if (int.TryParse(enteredKey, out _))
            {
                int selectedKey = int.Parse(enteredKey);
                return list[selectedKey];
            }
            else
            {
                Helpers.Print("Enter Numerical option");
                return SelectValidDetails(label, list);
            }
        }
        public string GetValidDetails(string label)
        {
            bool inValidDetails = true;
            string inputData = string.Empty;
            if (string.IsNullOrEmpty(label))
            {
                Helpers.Print("Enter Value");
                return GetValidDetails(label);
            }
            else
            {
                while (inValidDetails)
                {
                    Helpers.Print($"Enter {label}");
                    inputData = Console.ReadLine()!;
                    inValidDetails = true;
                    if (string.Equals(label, "dateOfBirth"))
                    {
                        DateTime val;
                        DateTime today = DateTime.Today;
                        if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                        {
                            Helpers.Print("Invalid date format. The Correct format is dd/mm/yyyy");
                        }
                        else
                        {
                            int age = today.Year - DateTime.Parse(inputData).Year;
                            if (age < 18)
                            {
                                Helpers.Print("Age is not sufficient");
                            }
                            else
                            {
                                inValidDetails = false;
                            }
                        }
                    }
                    if (string.Equals(label, "email"))
                    {
                        Regex formatOfEmail = new Regex("^[a-zA-Z0-9._%+-]+@tezo.com$");
                        if (!formatOfEmail.IsMatch(inputData))
                        {
                            Helpers.Print("Inavalid Email Format");
                        }
                    }
                    if (string.Equals(label, "mobileNumber"))
                    {
                        if (inputData.Length != 10 || int.TryParse(inputData, out _))
                        {
                            Helpers.Print("Enter 10 digits Mobile Number");
                        }
                        else
                        {
                            inValidDetails = false;
                        }
                    }
                    if (string.Equals(label, "dateOfJoin"))
                    {
                        DateTime val;
                        if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                        {
                            Helpers.Print("Invalid date format.");
                        }
                        else
                        {
                            inValidDetails = false;
                        }
                    }
                    if (string.Equals(label, "firstName"))
                    {
                        if (inputData!.Length != 0)
                        {
                            inValidDetails = false;
                        }
                    }
                    if (string.Equals(label, "lastName"))
                    {
                        if (inputData!.Length != 0)
                        {
                            inValidDetails = false;
                        }
                    }
                }
                return inputData;
            }
        }
        public void DeleteEmployee()
        {
            if (string.IsNullOrEmpty(_employeeJsonData))
            {
                Helpers.Print("DataBase is Empty");
            }
            else
            {
                List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(_employeeJsonData)!;
                Helpers.Print("Enter Employee Id To Edit");
                string? enteredEmpId = Console.ReadLine();
                if (!string.IsNullOrEmpty(enteredEmpId))
                {
                    enteredEmpId = enteredEmpId!.ToUpper();
                    List<Employee> employeeData = JsonSerializer.Deserialize<List<Employee>>(_employeeJsonData)!;
                    Employee? employee = employeeData.Single(e => e.Id == enteredEmpId);
                    if (employee is not null)
                    {
                        _employeeManagementSystem.DeleteEmployee(enteredEmpId, employees);
                    }
                    else
                    {
                        Helpers.Print("Entered EmpId not in list");
                    }
                }
                else
                {
                    DeleteEmployee();
                }
            }
        }
    }
}