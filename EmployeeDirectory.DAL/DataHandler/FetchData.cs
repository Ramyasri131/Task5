using EmployeeDirectory.DAL.Extensions;
using System.Text.Json;

namespace EmployeeDirectory.DAL.Data
{
    public static class FetchData
    {
        public static List<Models.Employee> GetEmployeeDetails()
        {
            string employeeJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\CSharpCloneCopy\\Task5\\EmployeeDirectory.DAL\\StaticData\\Employee.json");
                return JsonSerializer.Deserialize<List<Models.Employee>>(employeeJsonData)!;
        }

        public static List<Models.Role> GetRoleDetails()
        {
            string roleJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\CSharpCloneCopy\\Task5\\EmployeeDirectory.DAL\\StaticData\\Role.json");
            if (roleJsonData.IsNullOrEmptyOrWhiteSpace())
            {
                return new List<Models.Role>();
            }
            else
            {
                return JsonSerializer.Deserialize<List<Models.Role>>(roleJsonData)!;
            }
        }
    }
}