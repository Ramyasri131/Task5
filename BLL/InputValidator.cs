using EmployeeDirectory.Utilities;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EmployeeDirectory.Validation
{
    public class InputValidator
    {
        Helpers helper = new Helpers();
        public void IsValid(string inputData, string labelData, out bool invalidDetails)
        {
            if (inputData.Length != 0)
            {
                invalidDetails = false;
            }
            else
            {
                invalidDetails = true;
                helper.Print($"Please Enter  + {labelData}");
            }
        }

        public string GetValidDetails(string label)
        {
            bool inValidDetails = true;
            string inputData = "";
            while (inValidDetails)
            {
                InputValidator valid = new InputValidator();
                helper.Print($"Enter {label}");
                inputData = Console.ReadLine();
                valid.IsValid(inputData, label, out inValidDetails);
                if (label == "dateOfBirth")
                {
                    DateTime val;
                    DateTime today = DateTime.Today;
                    if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                    {
                        helper.Print("Invalid date format. The Correct format is dd/mm/yyyy");
                        inValidDetails = true;
                    }
                    else
                    {
                        int age = today.Year - DateTime.Parse(inputData).Year;
                        if (age < 18)
                        {
                            helper.Print("Age is not sufficient");
                            inValidDetails = true;
                        }
                        else
                        {
                            inValidDetails = false;
                        }
                    }
                }
                if (label == "email")
                {
                    Regex formatOfEmail = new Regex("^[a-zA-Z0-9._%+-]+@tezo.com$");
                    if (!formatOfEmail.IsMatch(inputData))
                    {
                        helper.Print("Inavalid Email Format");
                        inValidDetails = true;
                    }
                }
                if (label == "mobileNumber")
                {
                    if (inputData.Length != 10 || int.TryParse(inputData, out _))
                    {
                        helper.Print("Enter 10 digits Mobile Number");
                        inValidDetails = true;
                    }
                    else
                    {
                        inValidDetails = false;
                    }
                }
                if (label == "dateOfJoin")
                {
                    DateTime val;
                    valid.IsValid(inputData, "date of join", out inValidDetails);
                    if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                    {
                        helper.Print("Invalid date format.");
                        inValidDetails = true;
                        helper.Print($"Parsed date: {inputData}");
                    }
                    else
                    {
                        inValidDetails = false;
                    }
                }
            }
            return inputData;
        }

        public string SelectValidDetails(string label, Dictionary<int, string> list)
        {
            helper.Print($"Select {label}");
            foreach (KeyValuePair<int, string> item in list)
            {
                helper.Print(item.Key + " " + item.Value);
            }
            int selectedKey = int.Parse(Console.ReadLine());
            return list[selectedKey];
        }
    }
}