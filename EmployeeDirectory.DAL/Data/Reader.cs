using System.Text.Json;

namespace EmployeeDirectory.DLL.Data
{
    public static class Reader
    {
        public static List<Models.Employee> GetEmployeeDetails()
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Employee.json");
            List<Models.Employee> employees =  JsonSerializer.Deserialize<List<DLL.Models.Employee>>(employeeJsonData)!;
            return employees;
        }

        public static List<Models.Role> GetRoleDetails()
        {
            string roleJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Role.json");
            List<DLL.Models.Role> RoleData = JsonSerializer.Deserialize<List<DLL.Models.Role>>(roleJsonData)!;
            return RoleData;
        }
    }
}