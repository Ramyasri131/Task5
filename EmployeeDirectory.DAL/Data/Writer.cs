using System.Text.Json;

namespace EmployeeDirectory.DAL.Data
{
    public static class Writer
    {
        public static void WriteEmployeeData(List<Models.Employee> employees)
        {
            string json = JsonSerializer.Serialize(employees);
            File.WriteAllText("C:\\Workspace\\Tasks\\CSharpCloneCopy\\Task5\\EmployeeDirectory.DAL\\StaticData\\Employee.json", json);
        }

        public static void WriteRoleData(List<Models.Role> roles)
        {
            string json = JsonSerializer.Serialize(roles);
            File.WriteAllText("C:\\Workspace\\Tasks\\CSharpCloneCopy\\Task5\\EmployeeDirectory.DAL\\StaticData\\Role.json", json);
        }
    }
}