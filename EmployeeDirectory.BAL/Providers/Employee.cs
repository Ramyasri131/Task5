using System.Data;
using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.DAL.Extensions;
using EmployeeDirectory.DAL.Exceptions;
using EmployeeDirectory.DAL.Data;

namespace EmployeeDirectory.BAL.Providers
{
    public static class Employee
    {
        public static void AddEmployee(DTO.Employee employee)
        {
            List<DAL.Models.Employee> employees;
            employees = Reader.GetEmployeeDetails();
            employees = employees.OrderBy(x => x.Id).ToList();
            int employeeCount = int.Parse(employees[^1].Id[2..]) + 1;
            string id = string.Format("{0:0000}", employeeCount);
            id = "TZ" + id;
            DAL.Models.Employee user = new()
            {
                Id = id,
                FirstName = employee.FirstName!,
                LastName = employee.LastName!,
                DateOfBirth = employee.DateOfBirth!,
                Manager = employee.Manager,
                MobileNumber = employee.MobileNumber,
                DateOfJoin = employee.DateOfJoin!,
                Email = employee.Email!,
                Location = employee.Location,
                JobTitle = employee.JobTitle,
                Department = employee.Department,
                Project = employee.Project,
            };
            employees.Add(user);
            Writer.WriteEmployeeData(employees);
        }

        public static List<DAL.Models.Employee> GetEmployees()
        {
            List<DAL.Models.Employee> employees;
            try
            {
                employees = Reader.GetEmployeeDetails();
            }
            catch (Exception)
            {
                throw new RecordNotFound("Data Base is empty");
            }
            return employees;
        }

        public static void EditEmployeeDetails(string selectedData, string? id, int selectedOption)
        {
            List<DAL.Models.Employee> employees;
            employees = Reader.GetEmployeeDetails();
            if (id.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidData("Enter Employee Id");
            }
            id = id?.ToUpper();
            DAL.Models.Employee? employee = employees.Where(e => e.Id!.Equals(id)).FirstOrDefault() ?? throw new RecordNotFound("Employee Id not found");

            // Mobile number is of type long, using following delegate doesn't works. So having the conditional statement.
            if (selectedOption != 4)
            {
                List<Action<DAL.Models.Employee, string>> modifyEmployeeData = new()
                {
                      (item, selectedData) => item.FirstName = selectedData,
                      (item, selectedData) => item.LastName = selectedData,
                      (item, selectedData) => item.Email = selectedData,
                      (item, selectedData) => item.DateOfBirth = selectedData,
                      (item, selectedData) => item.DateOfJoin = selectedData,
                      (item, selectedData) => item.Location = selectedData,
                      (item, selectedData) => item.JobTitle = selectedData,
                      (item, selectedData) => item.Department = selectedData,
                      (item, selectedData) => item.Manager = selectedData,
                      (item, selectedData) => item.Project = selectedData
                };
                modifyEmployeeData[selectedOption - 1](employee!, selectedData);
            }
            else
            {
                try
                {
                    employee.MobileNumber = long.Parse(selectedData);
                }
                catch (FormatException)
                {
                    throw;
                }
            }
            Writer.WriteEmployeeData(employees);
        }

        public static void DeleteEmployee(string? id)
        {
            List<DAL.Models.Employee> employees;
            employees = Reader.GetEmployeeDetails();
            DAL.Models.Employee? employee = GetEmployeeById(id);
            employees.RemoveAll(emp => emp.Id == employee.Id);
            Writer.WriteEmployeeData(employees);
        }

        public static DAL.Models.Employee GetEmployeeById(string? id)
        {
            if (id.IsNullOrEmptyOrWhiteSpace())
            {
                throw new BAL.Exceptions.InvalidData("Invalid Employee Id");
            }
            else
            {
                List<DAL.Models.Employee>? employees;
                employees = Reader.GetEmployeeDetails();
                id = id!.ToUpper();
                DAL.Models.Employee? employee = employees.Where(e => e.Id == id).FirstOrDefault();
                if (employee is null)
                {
                    throw new RecordNotFound("Employee not found");
                }
                else
                {
                    return employee;
                }
            }
        }
    }
}