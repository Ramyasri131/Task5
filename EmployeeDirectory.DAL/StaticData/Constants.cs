using EmployeeDirectory.DAL.Models;
using System.Text.Json;
using EmployeeDirectory.DAL.Data;
namespace EmployeeDirectory.DAL.StaticData
{
    public static class Constants
    {
        public static Dictionary<int, string> Departments = new()
        {
          {1,"PE" },
          {2,"QA" },
          {3,"Marketing" }
        };

        public static Dictionary<int, string> Projects = new()
        {
           {1,"Project1" },
           { 2,"Project2" },
           {3,"Project3" }
        };

        public static Dictionary<int, string> Managers = new()
        {
            {1,"Sandeep" },
            {2,"Siva" },
            {3,"Shashank" }
        };

        public static Dictionary<int, string> Locations = new()
        {
            {1,"Hyderabad" },
            {2,"Banglore" },
            {3,"Vizag" }
        };

        public static Dictionary<int, string> EmployeeDataLabels = new()
        {
           {1,"FirstName" },
           {2,"LastName" },
           {3,"Email" },
           {4,"MobileNumber" },
           {5, "DateOfBirth" },
           {6,"DateOfJoin" },
           {7,"Location" },
           {8,"JobTitle" },
           {9,"Department" },
           {10,"Manager" },
           {11,"Project" }
        };

        public static Dictionary<int, string> MainMenu = new()
        {
            {1,"Employee Management" },
            {2,"Role Management" },
            {3,"Exit" }
        };
        
        public static Dictionary<int, string> EmployeeManagementMenu = new()
        {
            {1,"Add employee" },
            {2,"Display All" },
            {3,"Display One"},
            {4,"Edit employee"},
            {5,"Delete employee"},
            {6,"Go Back"}
        };

        public static Dictionary<int, string> RoleManagementMenu = new()
        {
            {1,"Add Role" },
            {2,"Display All" },
            {3,"Go Back" }
        };

        public static Dictionary<int, string> Roles = [];

        public static int i = 1;

        public static void GetRoles()
        {
            List<string> rolesData = new();
            string roleJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\CSharpCloneCopy\\Task5\\EmployeeDirectory.DAL\\StaticData\\Role.json");
            List<Role> RoleData = JsonSerializer.Deserialize<List<Role>>(roleJsonData)!;
            foreach (Role item in RoleData)
            {
                rolesData.Add(item.Name!);
            }
            foreach (string roleName in rolesData)
            {
                Roles.Add(i, roleName);
                i++;
            }
        }

    }
}