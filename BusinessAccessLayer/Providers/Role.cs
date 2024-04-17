using System.Text.Json;

namespace EmployeeDirectory.BAL.Providers
{
    public class Role
    {
        public readonly string roleJsonData = File.ReadAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Role.json");

        public void AddRole(DLL.Models.Role roleInput)
        {
            List<DLL.Models.Role> inputRoleData = JsonSerializer.Deserialize<List<DLL.Models.Role>>(roleJsonData)!;
            inputRoleData.Add(roleInput);
            string inputTojson = JsonSerializer.Serialize(inputRoleData);
            File.WriteAllText("C:\\Workspace\\Tasks\\Task5CloneCopy\\Task5\\DataAccessLayer\\StaticData\\Role.json", inputTojson);
        }

        public List<DLL.Models.Role>? GetRoles()
        {
            List<DLL.Models.Role>? inputRoleData;
            inputRoleData = string.IsNullOrEmpty(roleJsonData) ? null : JsonSerializer.Deserialize<List<DLL.Models.Role>>(roleJsonData); ;
            return inputRoleData;
        }
    }
}