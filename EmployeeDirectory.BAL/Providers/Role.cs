using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.DAL.Data;
using EmployeeDirectory.DAL.Extensions;
using EmployeeDirectory.DAL.Exceptions;
using System.Text.Json;


namespace EmployeeDirectory.BAL.Providers
{
    public static class Role
    {
        public static void AddRole(DAL.Models.Role roleInput)
        {
            if(roleInput.Name.IsNullOrEmptyOrWhiteSpace())
            {
                throw new BAL.Exceptions.InvalidData("Enter Role Name");
            }
            List<DAL.Models.Role> inputRoleData;
            inputRoleData = Reader.GetRoleDetails();
            foreach (DAL.Models.Role role in inputRoleData)
            {
                if(role.Name == roleInput.Name)
                {
                    throw new BAL.Exceptions.InvalidData("Role Exists");
                }
            }
            inputRoleData.Add(roleInput);
            Writer.WriteRoleData(inputRoleData);
        }

        public static List<DAL.Models.Role> GetRoles()
        {
            try
            {
                return Reader.GetRoleDetails();
            }
            catch (RecordNotFound)
            {
                throw new RecordNotFound("Data Base is empty");
            }
            catch(JsonException)
            {
                throw;
            }
        }
    }
}