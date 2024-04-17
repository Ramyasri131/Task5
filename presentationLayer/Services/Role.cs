using EmployeeDirectory.Utilities;
using EmployeeDirectory.DLL.StaticData;

namespace EmployeeDirectory.Services
{
    public class Role
    {
        private readonly BAL.Providers.Role _roleManagementSystem = new();
        private readonly DisplayHelper _printer = new();
        public void CollectRoleDetails()
        {
            Helpers.Print("Enter RoleName");
            string roleName = "";
            try
            {
                roleName = Console.ReadLine()!;
                if (string.IsNullOrEmpty(roleName) || string.IsNullOrEmpty(roleName))
                {
                    throw new ArgumentException();
                }
            }
             catch (ArgumentException ex)
            {
                Helpers.Print(ex.ToString());
                CollectRoleDetails();
            }
            Helpers.Print("select department");
            string department = SaveValidDetails("department", Constant.Departments);
            Helpers.Print("Enter Description");
            string? description = Console.ReadLine();
            Helpers.Print("Select Location");
            string location = SaveValidDetails("location", Constant.Locations);
            DLL.Models.Role roleInput = new()
            {
                Name = roleName,
                Location = location,
                Department = department,
                Description = description
            };
            _roleManagementSystem.AddRole(roleInput);

        }
        public static string SaveValidDetails(string label, Dictionary<int, string> list)
        {
            foreach (KeyValuePair<int, string> item in list)
            {
                Helpers.Print(item.Key + " " + item.Value);
            }
            string? enteredKey = Console.ReadLine();
            int selectedKey = int.Parse(enteredKey!);
            return list[selectedKey];
        }

        public void DisplayRoles()
        {
            List<DLL.Models.Role>? roleData;
            roleData = _roleManagementSystem.GetRoles();
            _printer.PrintRoleData(roleData);
        }
    }
}