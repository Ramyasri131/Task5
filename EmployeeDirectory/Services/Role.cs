using EmployeeDirectory.Utilities;
using EmployeeDirectory.DAL.StaticData;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.DAL.Exceptions;
using System.Text.Json;

namespace EmployeeDirectory.Services
{
    public class Role:IRoleService
    {
        public  void CollectRoleDetails()
        {
            Helpers.Print("Enter RoleName");
            string? roleName = Console.ReadLine();
            DAL.Models.Role roleInput;
            try
            {
                Helpers.Print("select department");
                string department = SaveValidDetails("department", Constant.Departments);
                Helpers.Print("Enter Description");
                string? description = Console.ReadLine();
                Helpers.Print("Select Location");
                string location = SaveValidDetails("location", Constant.Locations);
                roleInput = new()
                {
                    Name = roleName,
                    Location = location,
                    Department = department,
                    Description = description
                };
            }
            catch (FormatException)
            {
                throw;
            }
            try
            {
                BAL.Providers.Role.AddRole(roleInput);
            }
            catch (BAL.Exceptions.InvalidData)
            {
                throw;
            }
        }

        public static string SaveValidDetails(string label, Dictionary<int, string> list)
        {
            foreach (KeyValuePair<int, string> item in list)
            {
                Helpers.Print(item.Key + " " + item.Value);
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
                DisplayHelper.PrintRoleData(roleData);
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