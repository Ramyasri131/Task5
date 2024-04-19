using System.Data;
using System.Text.RegularExpressions;
using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.DLL.Data;

namespace EmployeeDirectory.BAL.Providers
{
    public static class Employee
    {
        public static void AddEmployee(DLL.Models.Employee employee)
        {
            List<DLL.Models.Employee> employees = Reader.GetEmployeeDetails();
            int noOfEmployees = employees.Count + 1;
            string id = string.Format("{0:0000}", noOfEmployees);
            id = "TZ" + id;
            employee.Id = id;
            employees.Add(employee);
            Writer.WriteEmployeeData(employees);
        }

        public static List<DLL.Models.Employee> GetEmployees()
        {
            List<DLL.Models.Employee> employees = Reader.GetEmployeeDetails();
            if (employees.Count == 0)
            {
                throw new EmptyDataBase();
            }
            return employees;
        }

        public static void EditEmployeeDetails(string selectedData, string? id, int selectedOption)
        {
            List<DLL.Models.Employee> employees = Reader.GetEmployeeDetails();
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidEmployeeId("Enter Employee Id");
            }
            Regex formatOfId = new Regex(@"^(?i)tz\d{4}$");
            if (!formatOfId.IsMatch(id))
            {
                throw new InvalidEmployeeId("Enter Valid Employee Id");
            }
            List<Action<DLL.Models.Employee, string>> modifyEmployeeData = new()
            {
                  (item, selectedData) => item.FirstName = selectedData,
                  (item, selectedData) => item.LastName = selectedData,
                  (item, selectedData) => item.Email = selectedData,
                  (item, selectedData) => item.MobileNumber = selectedData,
                  (item, selectedData) => item.DateOfBirth = selectedData,
                  (item,selectedData) => item.DateOfJoin = selectedData,
                  (item, selectedData) => item.Location = selectedData,
                  (item, selectedData) => item.JobTitle = selectedData,
                  (item, selectedData) => item.Department = selectedData,
                  (item, selectedData) => item.Manager = selectedData,
                  (item, selectedData) => item.Project = selectedData
            };
            id = id?.ToUpper();
            DLL.Models.Employee? employee = employees.Where(e => e.Id!.Equals(id)).FirstOrDefault();
            modifyEmployeeData[selectedOption - 1](employee!, selectedData);
            Writer.WriteEmployeeData(employees);
        }

        public static void DeleteEmployee(string? id)
        {
            List<DLL.Models.Employee>? employees = Reader.GetEmployeeDetails();
            if(employees == null || employees.Count == 0)
            {
                throw new EmptyDataBase();
            }
            DLL.Models.Employee? employee = GetEmployeeById(id);
            employees.RemoveAll(emp => emp.Id == employee.Id);
            Writer.WriteEmployeeData(employees);
        }

        public static DLL.Models.Employee GetEmployee(string? id)
        {
            List<DLL.Models.Employee>? employees = Reader.GetEmployeeDetails();
            if (employees == null || employees.Count == 0)
            {
                throw new EmptyDataBase();
            }
            DLL.Models.Employee? employee = GetEmployeeById(id);
            return employee;
        }

        public static DLL.Models.Employee GetEmployeeById(string? id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidEmployeeId("Invalid Employee Id");
            }
            else
            {
                List<DLL.Models.Employee> employees = Reader.GetEmployeeDetails();
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