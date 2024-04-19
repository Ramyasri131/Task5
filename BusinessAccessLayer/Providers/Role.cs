using System.Text.Json;
using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.DLL.Data;

namespace EmployeeDirectory.BAL.Providers
{
    public static class Role
    {
        public static void AddRole(DLL.Models.Role roleInput)
        {
            if(string.IsNullOrEmpty(roleInput.Name) || string.IsNullOrWhiteSpace(roleInput.Name))
            {
                throw new InvalidRoleName();
            }
            List<DLL.Models.Role> inputRoleData = Reader.GetRoleDetails();
            inputRoleData.Add(roleInput);
            Writer.WriteRoleData(inputRoleData);
        }

        public static List<DLL.Models.Role>? GetRoles()
        {
            List<DLL.Models.Role>? inputRoleData = Reader.GetRoleDetails();
            if (inputRoleData.Count == 0)
            {
                throw new EmptyDataBase();
            }
            return inputRoleData;
        }
    }
}