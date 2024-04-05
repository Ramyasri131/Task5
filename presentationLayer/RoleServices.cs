using EmployeeDirectory.RoleData;
using EmployeeDirectory.Utilities;
using EmployeeDirectory.RoleManagement;
using EmployeeDirectory.ConstantData;
using EmployeeDirectory.Printing;

namespace EmployeeDirectory.RoleDetails
{
    public class RoleServices
    {
        private readonly RoleManagementSystem _roleManagementSystem= new();
        private readonly DisplayHelper _printer = new();
        public void CollectRoleDetails()
        {
            Helpers.Print("Enter RoleName");
            string roleName;
            while (true)
            {
                roleName = Console.ReadLine()!;
                if (string.IsNullOrEmpty(roleName))
                {
                    Helpers.Print("Enter RoleName");
                }
                else
                {
                    break;
                }
            }
            Helpers.Print("select department");
            string department = SaveValidDetails("department", Constant.Departments);
            Helpers.Print("Enter Description");
            string? description = Console.ReadLine();
            Helpers.Print("Select Location");
            string location = SaveValidDetails("location", Constant.Locations);
            Role roleInput = new()
            {
                Name = roleName,
                Location = location,
                Department = department,
                Description = description
            };
            _roleManagementSystem.AddRole(roleInput);
            Constant.GetRoles();
        }
        public static string SaveValidDetails(string label, Dictionary<int, string> list)
        {
            foreach (KeyValuePair<int, string> item in list)
            {
                Helpers.Print(item.Key + " " + item.Value);
            }
            string? enteredKey = Console.ReadLine();
            if (int.TryParse(enteredKey, out _))
            {
                int selectedKey = int.Parse(enteredKey);
                return list[selectedKey];
            }
            else
            {
                Helpers.Print("Enter Numerical option");
                return SaveValidDetails(label, list);
            }
        }

        public void DisplayRoles()
        {
            List<Role>? roleData;
            roleData = _roleManagementSystem.GetRoles();
            _printer.PrintRoleData(roleData);
        }
    }
}