using EmployeeDirectory.DAL.Data;
using EmployeeDirectory.BAL.Extensions;
using EmployeeDirectory.DAL.Exceptions;


namespace EmployeeDirectory.BAL.Providers
{
    public static class RoleProvider
    {
        public static void AddRole(DAL.Models.Role roleInput)
        {
            if(roleInput.Name.IsNullOrEmptyOrWhiteSpace())
            {
                throw new BAL.Exceptions.InvalidData("Enter Role Name");
            }
            List<DAL.Models.Role> inputRoleData;
            inputRoleData = FetchData.GetRoleDetails();
            foreach (DAL.Models.Role role in inputRoleData)
            {
                if(role.Name == roleInput.Name)
                {
                    throw new BAL.Exceptions.InvalidData("Role Exists");
                }
            }
            inputRoleData.Add(roleInput);
            UpdateData.UpdateRoleData(inputRoleData);
        }

        public static List<DAL.Models.Role> GetRoles()
        {
            List < DAL.Models.Role > roles= FetchData.GetRoleDetails();
            if(roles.Count==0)
            {
                throw new RecordNotFound("Data Base is empty");
            }
            else
            {
                return roles;
            }
        }
    }
}