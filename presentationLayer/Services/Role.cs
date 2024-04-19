using EmployeeDirectory.Utilities;
using EmployeeDirectory.DLL.StaticData;
using EmployeeDirectory.BAL.Exceptions;

namespace EmployeeDirectory.Services
{
    public interface IRoleService
    {
        public void CollectRoleDetails();
        public void DisplayRoles();
    }

    public class Role:IRoleService
    {
        public  void CollectRoleDetails()
        {
            Helpers.Print("Enter RoleName");
            string? roleName = Console.ReadLine();
            DLL.Models.Role roleInput;
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
            catch (InvalidRoleName)
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
            int selectedKey=0;
            try
            {
                selectedKey = int.Parse(Console.ReadLine()!);
                return list[selectedKey];
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public void DisplayRoles()
        {
            List<DLL.Models.Role>? roleData;
            try
            {
                roleData = BAL.Providers.Role.GetRoles();
                DisplayHelper.PrintRoleData(roleData);
            }
            catch(EmptyDataBase)
            {
                throw;
            }
           
        }
    }
}