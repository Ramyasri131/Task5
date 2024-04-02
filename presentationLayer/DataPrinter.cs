using EmployeeDirectory.Constant;
using EmployeeDirectory.Utilities;
using System.Data;

namespace EmployeeDirectory.Printing
{
    public class DataPrinter
    {
        Helpers helper= new Helpers();
        public void PrintEmployeeData(List<EmployeeData>employeeData)
        {
            if(employeeData==null)
            {
                helper.Print("No employee present in DataBase");
            }
            else
            {
                foreach (EmployeeData employee in employeeData)
                {
                    helper.Print("");
                    helper.Print($"Employee Id:  {employee.empId}");
                    helper.Print($"FirstName :  {employee.firstName}");
                    helper.Print($"Last Name:   {employee.lastName}");
                    helper.Print($"Date of birth: {employee.dateOfBirth}");
                    helper.Print($"Email:   {employee.email}");
                    helper.Print($"Mobile Number :  {employee.mobileNumber}");
                    helper.Print($"Date of Join :  {employee.dateOfJoin}");
                    helper.Print($"Location :  {employee.location}");
                    helper.Print($"Job Title :  {employee.jobTitle}");
                    helper.Print($"Department :  {employee.department}");
                    helper.Print($"Manager:  {employee.manager}");
                    helper.Print($"Project :  {employee.project}");
                    helper.Print("==================================");
                }
            }
            
        }
        public void PrintEmployeeData(EmployeeData employee)
        {
                helper.Print("");
                helper.Print($"Employee Id:  {employee.empId}");
                helper.Print($"FirstName :  {employee.firstName}");
                helper.Print($"Last Name:   {employee.lastName}");
                helper.Print($"Date of birth: {employee.dateOfBirth}");
                helper.Print($"Email:   {employee.email}");
                helper.Print($"Mobile Number :  {employee.mobileNumber}");
                helper.Print($"Date of Join :  {employee.dateOfJoin}");
                helper.Print($"Location :  {employee.location}");
                helper.Print($"Job Title :  {employee.jobTitle}");
                helper.Print($"Department :  {employee.department}");
                helper.Print($"Manager:  {employee.manager}");
                helper.Print($"Project :  {employee.project}");
                helper.Print("==================================");
        }

        public void PrinRoleData(List<RoleData>roleData) {
            if(roleData==null)
            {
                helper.Print("No roles present in Data");
            }
            else
            {
                foreach (RoleData item in roleData)
                {
                    helper.Print($"Role Name: {item.roleName}");
                    helper.Print($"Location:  {item.location}");
                    helper.Print($"Department:  {item.department}");
                    helper.Print($"Description:  {item.description}");
                    helper.Print("============================");
                }
            }
            
        }
    }
}
