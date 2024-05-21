using EmployeeDirectory.Utilities;
using EmployeeDirectory.DAL.StaticData;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.DAL.Exceptions;
using System.Text.Json;

namespace EmployeeDirectory.Services
{
    public class Role:IRoleService
    {
        public  void GetDetails()
        {
            Display.Print("Enter RoleName");
            string? roleName = Console.ReadLine();
            DAL.Models.Role roleInput;
            try
            {
                Display.Print("select department");
                string department = SaveDetails("department", Constants.Departments);
                Display.Print("Enter Description");
                string? description = Console.ReadLine();
                Display.Print("Select Location");
                string location = SaveDetails("location", Constants.Locations);
                roleInput = new()
                {
                    Name = roleName,
                    Location = location,
                    Department = department,
                    Description = description
                };
                BAL.Providers.Role.AddRole(roleInput);
            }
            catch (FormatException)
            {
                throw;
            }
            catch (BAL.Exceptions.InvalidData)
            {
                throw;
            }
        }

        public static string SaveDetails(string label, Dictionary<int, string> list)
        {
            foreach (KeyValuePair<int, string> item in list)
            {
                Display.Print(item.Key + " " + item.Value);
            }
            int selectedKey;
            try
            {
                selectedKey = int.Parse(Console.ReadLine()!);
                if( selectedKey > list.Count ) {
                    throw new BAL.Exceptions.InvalidData("Choose the option from the list");
                }
                return list[selectedKey];
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public void DisplayRoles()
        {
            List<DAL.Models.Role>? roleData;
            try
            {
                roleData = BAL.Providers.Role.GetRoles();
                DisplayData.PrintRoleData(roleData);
            }
            catch(RecordNotFound)
            {
                throw;
            }
            catch (JsonException)
            {
                throw;
            }
        }
    }
}