using System.Text.Json;
using System.Threading.Tasks;
using EmployeeDirectory.RoleData;

namespace EmployeeDirectory.RoleManagement
{
    public class RoleManagementSystem
    {
      
        public void AddRole(Role roleInput)
        {
            string roleJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\RoleData.json");
            List<Role> inputRoleData = JsonSerializer.Deserialize<List<Role>>(roleJsonData)!;
            inputRoleData.Add(roleInput);
            string inputTojson = JsonSerializer.Serialize(inputRoleData);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\RoleData.json", inputTojson);
        }

        public List<Role>? GetRoles()
        {
            string roleJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5\\DAL\\RoleData.json");
            List<Role>? inputRoleData;
            inputRoleData = string.IsNullOrEmpty(roleJsonData) ? null : JsonSerializer.Deserialize<List<Role>>(roleJsonData); ;
            return inputRoleData;
        }
    }
}