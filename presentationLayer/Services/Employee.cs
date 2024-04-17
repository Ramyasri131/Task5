using EmployeeDirectory.Utilities;
using EmployeeDirectory.DLL.StaticData;
using EmployeeDirectory.BAL.Validatior;
using EmployeeDirectory.BAL.Exceptions;

namespace EmployeeDirectory.Services
{
    public class Employee
    {
        private readonly DisplayHelper _printer = new();
        private readonly BAL.Providers.Employee _employeeManagementSystem = new();

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
            Console.WriteLine(employeeInput.Location);
            List<string> InvalidInputs = Input.IsValidEmployee(employeeInput);
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
            try
            {
                List<DLL.Models.Employee> employeeData = _employeeManagementSystem.GetAllEmployees();
                _printer.PrintEmployeesData(employeeData);
            }
            catch (Exception ex)
            {
                Helpers.Print(ex.ToString());
            }
        }

        public void DisplayEmployee()
        {
            BAL.Providers.Employee employee = new();
            DLL.Models.Employee employeeData = new();
            Helpers.Print("Enter Employee Id");
            string? id = Console.ReadLine();
            try
            {
                employeeData = employee.Display(id);
                _printer.PrintEmployeeData(employeeData);
            }
            catch (EmptyDataBase e)
            {
                Helpers.Print(e.ToString());
                DisplayEmployee();
            }

            catch (InvalidEmployeeId e)
            {
                Helpers.Print(e.ToString());
                DisplayEmployee();
            }
        }

        public void EditEmployee()
        {
            Helpers.Print("Enter Employee Id");
            string? id = Console.ReadLine();
            try
            {
                if (Input.IsEmployeePresent(id))
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
                            Constant.GetRoles();
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
                        _employeeManagementSystem.EditEmployeeDetails(dataToEdit, id, selectedOption);
                    }
                    else
                    {
                        Helpers.Print("Enter valid option");
                    }
                }
                else
                {
                    Helpers.Print("Entered Id is not present in Data");
                }
            }
            catch (Exception ex)
            {
                Helpers.Print(ex.ToString());
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
                if(selectedKey<=0 && selectedKey>=list.Count)
                {
                    Helpers.Print("Choose from the given options");
                    return SelectValidDetails(label, list);
                }
                else
                {
                    return list[selectedKey];
                }
            }
            else
            {
                Helpers.Print("Enter Numerical option");
                return SelectValidDetails(label, list);
            }
        }
        public string GetValidDetails(string label)
        {
            Helpers.Print($"Enter {label}");
            string? inputData = Console.ReadLine();
            try
            {
                string employeeData=Input.GetValidData(label,inputData);
                return inputData!;
            }
            catch (InvalidInput e)
            {
                Helpers.Print(e.ToString());
                return GetValidDetails(label);
            }
        }
        public void DeleteEmployee()
        {
            Helpers.Print("Enter Employee Id To Delete");
            string? enteredEmpId = Console.ReadLine();
            try
            {
                try
                {
                    _employeeManagementSystem.DeleteEmployee(enteredEmpId);
                }
                catch (EmptyDataBase ex) {
                    Helpers.Print(ex.ToString());
                    DeleteEmployee();
                }
            }
            catch (InvalidEmployeeId e)
            {
                Helpers.Print(e.ToString());
                DeleteEmployee();
            }
        }
    }
}