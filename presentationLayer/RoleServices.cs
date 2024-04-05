using EmployeeDirectory.RoleData;
using EmployeeDirectory.Utilities;
using EmployeeDirectory.RoleManagement;
using EmployeeDirectory.ConstantData;
using EmployeeDirectory.Printing;

namespace EmployeeDirectory.RoleDetails
{
    public class RoleServices
    { 
        private RoleManagementSystem _roleManagementSystem= new();
        private DataPrinter printer = new();
        public void CollectRoleDetails()
        {
            string roleName = string.Empty;
            Helpers.Print("Enter RoleName");
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
                location = location,
                department = department,
                description = description
            };
            _roleManagementSystem.AddRole(roleInput);
            Constant.GetRoles();
        }
        public string SaveValidDetails(string label, Dictionary<int, string> list)
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
            printer.PrintRoleData(roleData);
        }
    }
}