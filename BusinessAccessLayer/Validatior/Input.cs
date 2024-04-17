using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using EmployeeDirectory.DLL.Models;
using EmployeeDirectory.BAL.Exceptions;
using System.Text.Json;

namespace EmployeeDirectory.BAL.Validatior
{
    public static class Input
    {
        public static List<string> IsValidEmployee(Employee? employee)
        {
            List<string> invalidInputs = new();
            foreach (PropertyInfo propertyInfo in employee.GetType().GetProperties())
            {
                string? input = propertyInfo.GetValue(employee)!.ToString();
                if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
                {
                    switch (propertyInfo.Name) {
                        case "DateOfBirth":
                            DateTime val;
                            DateTime today = DateTime.Today;
                            if (!DateTime.TryParseExact(employee.DateOfBirth, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                            {
                                invalidInputs.Add("date of Birth");
                            }
                            else
                            {
                                int age = today.Year - DateTime.Parse(employee.DateOfBirth).Year;
                                if (age < 18)
                                {
                                    invalidInputs.Add("date of Birth");
                                }

                            }
                            break;
                        case "DateOfJoin":
                            if (!DateTime.TryParseExact(employee.DateOfJoin, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                            {
                                invalidInputs.Add("date of Join");
                            }
                            break;
                        case "Email":
                            Regex formatOfEmail = new Regex("^[a-zA-Z0-9._%+-]+@tezo.com$");
                            if (!formatOfEmail.IsMatch(employee.Email!))
                            {
                                invalidInputs.Add("Email");
                            }
                            break;
                        case "MobileNumber":
                            if (employee.MobileNumber!.Length != 10 || int.TryParse(employee.MobileNumber, out _))
                            {
                                invalidInputs.Add("Mobile Number");
                            }
                            break;
                    }
                }
                else
                {
                    invalidInputs.Add(propertyInfo.Name);
                }
            }
            return invalidInputs;
        }
        public static bool IsEmployeePresent(string? id)
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Employee.json");
            List<DLL.Models.Employee> employeeData = JsonSerializer.Deserialize<List<DLL.Models.Employee>>(employeeJsonData)!;
            if (employeeData.Count == 0)
            {
                throw new EmptyDataBase();
            }
            
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidEmployeeId("Invalid Employee Id");
            }
            else
            {
                id = id?.ToUpper();
                DLL.Models.Employee? employee = employeeData.Where(e => e.Id == id).FirstOrDefault();
                if (employee != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string GetValidData(string label,string? inputData)
        {
            if(string.IsNullOrEmpty(inputData) || string.IsNullOrWhiteSpace(inputData))
            {
                throw new InvalidInput();
            }
            switch (label)
            {
                case "dateOfBirth":
                    DateTime val;
                    DateTime today = DateTime.Today;
                    if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                    {
                        throw new InvalidInput();
                    }
                    else
                    {
                        int age = today.Year - DateTime.Parse(inputData).Year;
                        if (age < 18)
                        {
                            throw new InvalidInput();
                        }
                    }
                    break;
                case "email":
                    Regex formatOfEmail = new Regex("^[a-zA-Z0-9._%+-]+@tezo.com$");
                    if (!formatOfEmail.IsMatch(inputData))
                    {
                        throw new InvalidInput();
                    }
                    break;
                case "mobileNumber":
                    if (inputData.Length != 10 || int.TryParse(inputData, out _))
                    {
                        throw new InvalidInput();
                    }
                    break;
                case "dateOfJoin":
                    if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _))
                    {
                        throw new InvalidInput();
                    }
                    break;
                default:
                    if (inputData!.Length == 0)
                    {
                        throw new InvalidInput();
                    }
                    break;
            }
            return inputData;
        }
    }
}