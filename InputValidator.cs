using System.Globalization;
using EmployeeDirectory.EmployeeData;
using System.Text.RegularExpressions;

namespace EmployeeDirectory.Validation
{
    public class InputValidator
    {
        public bool IsValid(string? inputData)
        {
            return (string.IsNullOrEmpty(inputData) || string.IsNullOrWhiteSpace(inputData)) ?  false : true;
        }
        public List<string> IsValidEmployee(Employee employeeInput)
        {
            List<string> InvalidInputs =  new();
            bool isValid = IsValid(employeeInput.FirstName);
            if (!isValid)
            {
                InvalidInputs.Add("First Name");
            }
            isValid = IsValid(employeeInput.LastName);
            if (!isValid)
            {
                InvalidInputs.Add("Last Name");
            }
            isValid = IsValid(employeeInput.DateOfBirth);
            if (isValid)
            {
                DateTime val;
                DateTime today = DateTime.Today;
                if (!DateTime.TryParseExact(employeeInput.DateOfBirth, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                {
                    InvalidInputs.Add("date of Birth");
                }
                else
                {
                    int age = today.Year - DateTime.Parse(employeeInput.DateOfBirth).Year;
                    if (age < 18)
                    {
                        InvalidInputs.Add("date of Birth");
                    }

                }
            }
            else
            {
                InvalidInputs.Add("Date of Birth");
            }
            isValid = IsValid(employeeInput.DateOfJoin);
            if (isValid)
            {
                DateTime val;
                DateTime today = DateTime.Today;
                if (!DateTime.TryParseExact(employeeInput.DateOfJoin, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                {
                    InvalidInputs.Add("date of Join");
                }
            }
            else
            {
                InvalidInputs.Add("Date of Join");
            }
            isValid=IsValid(employeeInput.Email);
            if (isValid)
            {
                Regex formatOfEmail = new Regex("^[a-zA-Z0-9._%+-]+@tezo.com$");
                if (!formatOfEmail.IsMatch(employeeInput.Email!))
                {
                    InvalidInputs.Add("Email");
                }
            }
            else
            {
                InvalidInputs.Add("Email");
            }
            isValid = IsValid(employeeInput.MobileNumber);
            if (isValid)
            {
                if (employeeInput.MobileNumber!.Length != 10 || int.TryParse(employeeInput.MobileNumber, out _))
                {
                    InvalidInputs.Add("Mobile Number");
                }
            }
            else
            {
                InvalidInputs.Add("Mobile Number");
            }
            return InvalidInputs;
        }
    }
}