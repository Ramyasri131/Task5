using System.Text.Json;
using EmployeeDirectory.Validation;
using EmployeeDirectory.Constant;
using EmployeeDirectory.Utilities;

//when user selects an option
namespace EmployeeDirectory.UserActions
{
    public class UserActionHandler
    {
        Helpers helper = new Helpers();
        public void OnSelectAddEmployee()
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\employeeData.json");
            while (true)
            {
                bool inValidDetails = true;
                string empId = "";
                while (inValidDetails)
                {
                    InputValidator valid = new InputValidator();
                    helper.Print("Enter Employee Id");
                    empId = Console.ReadLine();
                    empId = empId!.ToUpper();
                    string PrefixOfEmpId = empId.Substring(0, 2);
                    string SufixOfEmpId = "";
                    valid.IsValid(empId, "Employee Id", out inValidDetails);
                    if (PrefixOfEmpId != "TZ" || !int.TryParse(empId.Substring(2), out _))
                    {
                        helper.Print("Enter Valid Employee Id");
                        inValidDetails = true;
                    }
                    else
                    {
                        SufixOfEmpId = empId.Substring(2);
                        if (SufixOfEmpId.Length != 4)
                        {
                            helper.Print("Enter Valid Employee Id");
                            inValidDetails = true;
                        }
                        else
                        {
                            if (employeeJsonData != "")
                            {
                                List<EmployeeData> getEmployees = JsonSerializer.Deserialize<List<EmployeeData>>(employeeJsonData);
                                foreach (EmployeeData item in getEmployees)
                                {
                                    if (item.empId == empId)
                                    {
                                        helper.Print("Employee already present");
                                        inValidDetails = true;
                                    }
                                }
                            }
                        }
                    }
                }
                InputValidator validator = new InputValidator();
                string firstName = validator.GetValidDetails("firstName");
                string lastName = validator.GetValidDetails("lastName");
                string dateOfBirth = validator.GetValidDetails("dateOfBirth");
                string email = validator.GetValidDetails("email");
                string mobileNumber = validator.GetValidDetails("mobileNumber");
                string dateOfJoin = validator.GetValidDetails("dateOfJoin");
                string location = validator.SelectValidDetails("location", LoactionList.locations);
                string jobTitle = validator.SelectValidDetails("jobTitle", RoleList.roles);
                string department = validator.SelectValidDetails("department", DepartmentList.departments);
                string manager = validator.SelectValidDetails("Manager", ManagerList.managers);
                string project = validator.SelectValidDetails("Project", ProjectList.projects);
                EmployeeData employeeInput = new EmployeeData()
                {
                    empId = empId,
                    firstName = firstName,
                    lastName = lastName,
                    dateOfBirth = dateOfBirth,
                    email = email,
                    mobileNumber = mobileNumber,
                    dateOfJoin = dateOfJoin,
                    location = location,
                    jobTitle = jobTitle,
                    department = department,
                    manager = manager,
                    project = project
                };
                List<EmployeeData> loadedEmployees;
                if (employeeJsonData == "")
                {
                    loadedEmployees = new List<EmployeeData>();
                }
                else
                {
                    loadedEmployees = JsonSerializer.Deserialize<List<EmployeeData>>(employeeJsonData);
                }
                loadedEmployees.Add(employeeInput);
                string json = JsonSerializer.Serialize(loadedEmployees);
                File.WriteAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\employeeData.json", json);
                return;
            }

        }

        public List<EmployeeData> OnDisplayAll()
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\employeeData.json");
            List<EmployeeData> loadedEmployees=null;
            if (employeeJsonData != "")
            {
                loadedEmployees = JsonSerializer.Deserialize<List<EmployeeData>>(employeeJsonData);
            }
            return loadedEmployees;
        }

        public EmployeeData OnSelectDisplayOne()
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\employeeData.json");
            EmployeeData employeeData = null; 
            if(employeeJsonData != "")
            {
                List<EmployeeData> loadedEmployees = JsonSerializer.Deserialize<List<EmployeeData>>(employeeJsonData);
                helper.Print("Enter Employee Id");
                bool isPresent = false;
                string enteredEmpId = Console.ReadLine().ToUpper();
                foreach (EmployeeData item in loadedEmployees)
                {
                    if (item.empId == enteredEmpId)
                    {
                        isPresent = true;
                        employeeData = item;
                    }
                }
                if(!isPresent)
                {

                    helper.Print("Entered Employee Not Present in the Data");
                    helper.Print("");
                }
                return employeeData;

            }
            else
            {
                helper.Print("DataBase is empty");
                return employeeData;
            }
        }

        public void OnSelectEditEmployee()          
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\employeeData.json");
            if (employeeJsonData == "")
            {
                helper.Print("DataBase is Empty");
            }
            else
            {
                List<EmployeeData> loadedEmployees = JsonSerializer.Deserialize<List<EmployeeData>>(employeeJsonData);
                helper.Print("Enter Employee Id To Edit");
                string enteredEmpId = Console.ReadLine().ToUpper();
                bool isEmployeePresent = false;
                foreach (EmployeeData employee in loadedEmployees)
                {
                    if (employee.empId == enteredEmpId)
                    {
                        isEmployeePresent = true;
                        helper.Print("Field to edit");
                        foreach (KeyValuePair<int,string> item in EmployeeDetailsLabelList.employeeDataLabels)
                        {
                            helper.Print(item.Key + " " + item.Value);
                        }
                        helper.Print("Enter Option");
                        int selectedOption = int.Parse(Console.ReadLine());
                        string label =EmployeeDetailsLabelList.employeeDataLabels[selectedOption];
                        InputValidator validator = new InputValidator();
                        string dataToEdit = "";
                        if (label == "location")
                        {
                            dataToEdit = validator.SelectValidDetails(label, LoactionList.locations);
                        }
                        else if (label == "jobTitle")
                        {
                            dataToEdit = validator.SelectValidDetails(label, RoleList.roles);
                        }
                        else if (label == "department")
                        {
                            dataToEdit = validator.SelectValidDetails(label, DepartmentList.departments);
                        }
                        else if (label == "project")
                        {
                            dataToEdit = validator.SelectValidDetails(label, ProjectList.projects);
                        }
                        else
                        {
                            dataToEdit = validator.GetValidDetails(label);
                        }
                        List<Action<EmployeeData, string>> chageEnteredData = new List<Action<EmployeeData, string>>
                        {
                            (item, dataToEdit) => item.firstName = dataToEdit,
                            (item, dataToEdit) => item.lastName = dataToEdit,
                            (item, dataToEdit) => item.email = dataToEdit,
                            (item, dataToEdit) => item.mobileNumber = dataToEdit,
                            (item, dataToEdit) => item.dateOfBirth = dataToEdit,
                            (item, dataToEdit) => item.dateOfJoin = dataToEdit,
                            (item, dataToEdit) => item.location = dataToEdit,
                            (item, dataToEdit) => item.jobTitle = dataToEdit,
                            (item, dataToEdit) => item.department = dataToEdit,
                            (item, dataToEdit) => item.manager = dataToEdit,
                            (item, dataToEdit) => item.project = dataToEdit
                        };
                        foreach (EmployeeData item in loadedEmployees)
                        {
                            if (item.empId == enteredEmpId)
                            {
                                chageEnteredData[selectedOption - 1](item, dataToEdit);
                            }
                        }
                        string json = JsonSerializer.Serialize(loadedEmployees);
                        File.WriteAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\employeeData.json", json);
                    }
                }
                if (!isEmployeePresent)
                {
                    helper.Print("No Employee with entered employee id");
                    helper.Print("");
                }
            }
        }

        public void OnSelectDeleteEmployee()   
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\employeeData.json");
            if (employeeJsonData == "")
            {
                helper.Print("DataBase is Empty");
                helper.Print("");
            }
            else
            {
                List<EmployeeData> loadedEmployees;
                if (employeeJsonData != "")
                {
                    loadedEmployees = JsonSerializer.Deserialize<List<EmployeeData>>(employeeJsonData);
                }
                else
                {
                    loadedEmployees = new List<EmployeeData>();
                }
                helper.Print("Enter employee Id to delete");
                string enteredEmpId = Console.ReadLine().ToUpper();
                bool isEmployeePresent = false;
                foreach (EmployeeData item in loadedEmployees)
                {
                    if (item.empId == enteredEmpId)
                    {
                        loadedEmployees.Remove(item);
                        isEmployeePresent = true;
                        break;
                    }
                }
                if (!isEmployeePresent)
                {
                    helper.Print("Employee not present");
                    helper.Print("");
                }
                string json = JsonSerializer.Serialize(loadedEmployees);
                File.WriteAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\employeeData.json", json);
            }
        }

        public void OnSelectAddRole()
        {
            string roleJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\roleData.json");
            InputValidator validator = new InputValidator();
            string roleName = validator.SelectValidDetails("jobTitle", RoleList.roles);
            string department = validator.SelectValidDetails("department", DepartmentList.departments);
            helper.Print("Enter Description");
            string description = Console.ReadLine();
            string location = validator.SelectValidDetails("location", LoactionList.locations);
            RoleData roleInput = new RoleData()
            {
                roleName = roleName,
                location = location,
                department = department,
                description = description
            };
            List<RoleData> inputRoleData = JsonSerializer.Deserialize<List<RoleData>>(roleJsonData);
            inputRoleData.Add(roleInput);
            string inputTojson = JsonSerializer.Serialize(inputRoleData);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\roleData.json", inputTojson);
        }

        public List<RoleData> OnSelectDisplayAllRole()
        {
            string roleJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\roleData.json");
            List<RoleData> inputRoleData;
            if (roleJsonData=="")
            {
                inputRoleData = null;
            }
            else
            {
                inputRoleData = JsonSerializer.Deserialize<List<RoleData>>(roleJsonData);
            }
            return inputRoleData;
        }
    }
}