using System.Text.Json;

namespace EmployeeDirectory.DLL.Data
{
    public static class Writer
    {
        public static void WriteEmployeeData(List<DLL.Models.Employee> employees)
        {
            string json = JsonSerializer.Serialize(employees);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Employee.json", json);
        }

        public static void WriteRoleData(List<DLL.Models.Role> roles)
        {
            string json = JsonSerializer.Serialize(roles);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Role.json", json);
        }
    }
}