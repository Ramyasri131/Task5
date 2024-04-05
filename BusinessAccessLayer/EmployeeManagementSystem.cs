using System.Text.Json;
using EmployeeDirectory.EmployeeData;

//when user selects an option
namespace EmployeeDirectory.EmployeeManagement
{
    public class EmployeeManagementSystem
    {
        public  void AddEmployee(Employee employee)
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\Employee.json");
            List<Employee> employees = string.IsNullOrEmpty(employeeJsonData)? new() : JsonSerializer.Deserialize<List<Employee>>(employeeJsonData)!;
            employees.Add(employee);
            string json = JsonSerializer.Serialize(employees);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\Employee.json", json);
            return;
        }

        public List<Employee> GetAllEmployees(string employeeJsonData)
        {
            List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(employeeJsonData)!;
            return employees;
        }

        public void EditEmployeeDetails(string dataToEdit, string employeeJsonData, string? enteredEmpId, int selectedOption)
        {
            List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(employeeJsonData)!;
            List<Action<Employee, string>> chageEnteredData = new()
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
            Employee employee = employees.Single(e => e.Id!.Equals(enteredEmpId));
            chageEnteredData[selectedOption - 1](employee, dataToEdit);
            string json = JsonSerializer.Serialize(employees);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\Employee.json", json);
        }

        public void DeleteEmployee(string enteredEmpId, List<Employee> employees)
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\Employee.json");
            Employee employee = employees.Single(e => e.Id!.Equals(enteredEmpId));
            employees.Remove(employee);
            string json = JsonSerializer.Serialize(employees);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\Employee.json", json);
        }
    }
}