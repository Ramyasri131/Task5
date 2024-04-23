using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.DAL.Extensions;

namespace EmployeeDirectory.BAL.Validators
{
    public static class EmployeeValidator
    {
        public static List<string> ValidateDetails(BAL.DTO.Employee employee)
        {
            List<string> invalidInputs = [];
            foreach (PropertyInfo propertyInfo in employee.GetType().GetProperties())
            {
                string? input = propertyInfo.GetValue(employee)?.ToString();
                if (!input.IsNullOrEmptyOrWhiteSpace())
                {
                    switch (propertyInfo.Name) {
                        case "DateOfBirth":
                            DateTime today = DateTime.Today;
                            if (!DateTime.TryParseExact(employee.DateOfBirth, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
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
                            if (!DateTime.TryParseExact(employee.DateOfJoin, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
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
                            if (employee.MobileNumber.ToString()!.Length != 10)
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

        public static void ValidateData(string label,string inputData)
        {
            if(inputData.IsNullOrEmptyOrWhiteSpace())
            {
                throw new Exceptions.InvalidData($"Please Enter {label}");
            }
            switch (label)
            {
                case "DateOfBirth":
                    DateTime val;
                    DateTime today = DateTime.Today;
                    if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                    {
                        throw new BAL.Exceptions.InvalidData("Please enter a valid date of birth");
                    }
                    else
                    {
                        int age = today.Year - DateTime.Parse(inputData).Year;
                        if (age < 18)
                        {
                            throw new BAL.Exceptions.InvalidData("Please enter a valid date of birth");
                        }
                    }
                    break;
                case "Email":
                    Regex formatOfEmail = new Regex("^[a-zA-Z0-9._%+-]+@tezo.com$");
                    if (!formatOfEmail.IsMatch(inputData))
                    {
                        throw new BAL.Exceptions.InvalidData("Please enter valid Email");
                    }
                    break;
                case "MobileNumber":
                    if (inputData.Length != 10 || int.TryParse(inputData, out _))
                    {
                        throw new BAL.Exceptions.InvalidData("Please enter valid mobile number");
                    }
                    break;
                case "DateOfJoin":
                    if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _))
                    {
                        throw new BAL.Exceptions.InvalidData("Please enter valid Joining Date");
                    }
                    break;
            }
        }
    }
}