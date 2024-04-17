using System.Data;
using System.Numerics;
using System.Text.Json;
using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.DLL.Models;


//when user selects an option
namespace EmployeeDirectory.BAL.Providers
{
    public class Employee
    {
        private string employeeJsonData;

        public Employee()
        {
            employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Employee.json");
        }
        public void AddEmployee(DLL.Models.Employee employee)
        {
            List<DLL.Models.Employee> employees = string.IsNullOrEmpty(employeeJsonData) ? new() : JsonSerializer.Deserialize<List<DLL.Models.Employee>>(employeeJsonData)!;
            int noOfEmployees = employees.Count + 1;
            string id = string.Format("{0:0000}", noOfEmployees);
            id = "TZ" + id;
            employee.Id= id;
            employees.Add(employee);
            string json = JsonSerializer.Serialize(employees);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Employee.json", json);
        }

        public List<DLL.Models.Employee> GetAllEmployees()
        {
            List<DLL.Models.Employee> employees = JsonSerializer.Deserialize<List<DLL.Models.Employee>>(employeeJsonData)!;
            if(employees.Count == 0)
            {
                throw new EmptyDataBase();
            }
            return employees;
        }

        public void EditEmployeeDetails(string dataToEdit, string? enteredEmpId, int selectedOption)
        {
            List<DLL.Models.Employee> employees = JsonSerializer.Deserialize<List<DLL.Models.Employee>>(employeeJsonData)!;
            List<Action<DLL.Models.Employee, string>> chageEnteredData = new()
                {
                      (item, dataToEdit) => item.FirstName = dataToEdit,
                      (item, dataToEdit) => item.LastName = dataToEdit,
                      (item, dataToEdit) => item.Email = dataToEdit,
                      (item, dataToEdit) => item.MobileNumber = dataToEdit,
                      (item, dataToEdit) => item.DateOfBirth = dataToEdit,
                      (item, dataToEdit) => item.DateOfJoin = dataToEdit,
                      (item, dataToEdit) => item.Location = dataToEdit,
                      (item, dataToEdit) => item.JobTitle = dataToEdit,
                      (item, dataToEdit) => item.Department = dataToEdit,
                      (item, dataToEdit) => item.Manager = dataToEdit,
                      (item, dataToEdit) => item.Project = dataToEdit
                };
            enteredEmpId = enteredEmpId?.ToUpper();
            DLL.Models.Employee? employee = employees.Where(e => e.Id!.Equals(enteredEmpId)).FirstOrDefault();
            chageEnteredData[selectedOption - 1](employee!, dataToEdit);
            string json = JsonSerializer.Serialize(employees);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Employee.json", json);
        }

        public void DeleteEmployee(string? id)
        {
            List<DLL.Models.Employee>? employees = JsonSerializer.Deserialize<List<DLL.Models.Employee>>(employeeJsonData);
            if(employees == null || employees.Count == 0)
            {
                throw new EmptyDataBase();
            }
            DLL.Models.Employee? employee = IsEmployeePresent(id);
            employees.RemoveAll(emp => emp.Id == employee.Id);
            string json = JsonSerializer.Serialize(employees);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Employee.json", json);
        }

        public DLL.Models.Employee Display(string? id)
        {
            List<DLL.Models.Employee>? employees = JsonSerializer.Deserialize<List<DLL.Models.Employee>>(employeeJsonData);
            if (employees == null || employees.Count == 0)
            {
                throw new EmptyDataBase();
            }
            DLL.Models.Employee? employee = IsEmployeePresent(id);
            return employee;
        }

        public static DLL.Models.Employee IsEmployeePresent(string? id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidEmployeeId("Invalid Employee Id");
            }
            else
            {
                string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Employee.json");
                List<DLL.Models.Employee> employees = JsonSerializer.Deserialize<List<DLL.Models.Employee>>(employeeJsonData)!;
                id = id.ToUpper();
                DLL.Models.Employee? employee = employees.Where(e => e.Id == id).FirstOrDefault();
                if (employee is null)
                {
                    throw new InvalidEmployeeId("Employee not present");
                }
                else
                {
                    return employee;
                }
            }
        }
    }
}