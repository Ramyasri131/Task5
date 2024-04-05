using EmployeeDirectory.RoleData;
using System.Text.Json;
namespace EmployeeDirectory.ConstantData
{
    public static class Constant
    {
        public static Dictionary<int, string> Departments = new()
        {
          {1,"PE" },
          { 2,"QA" },
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
            { 2,"Banglore" },
            {3,"Vizag" }
        };
        public static Dictionary<int, string> EmployeeDataLabels = new()
        {
           {1,"firstName" },
           {2,"lastName" },
           {3,"email" },
           {4,"mobileNumber" },
           {5, "dateOfBirth" },
           {6,"dateOfJoin" },
           {7,"location" },
           {8,"jobTitle" },
           {9,"department" },
           {10,"manager" },
           {11,"project" }
        };
        public static Dictionary<int, string> MainMenu = new()
        {
            {1,"Employee Management" },
            {2,"Role Management" },
            {3,"Exit" }
        };
        public static Dictionary<int, string> EmployeMaangementMenu = new()
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

        public static void GetRoles()
        {
            List<string> rolesData = new();
            string roleJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\RoleData.json");
            List<Role> RoleData = JsonSerializer.Deserialize<List<Role>>(roleJsonData)!;
            foreach (Role item in RoleData)
            {
                rolesData.Add(item.Name);
            }
            int i = 1;
            foreach (string roleName in rolesData)
            {
                Roles.Add(i,roleName);
                i++;
            }
        }
    }
}