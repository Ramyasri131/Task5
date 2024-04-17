using EmployeeDirectory.DLL.Models;
using System.Reflection;

namespace EmployeeDirectory.Utilities
{
    public class DisplayHelper
    {
        public void PrintEmployeesData(List<Employee> employeeData)
        {
            if (employeeData is null)
            {
                Helpers.Print("No employee present in DataBase");
            }
            else
            {
                foreach (Employee employee in employeeData)
                {
                    PrintEmployeeData(employee);
                }
            }
        }

        public void PrintEmployeeData(Employee employee)
        {
            foreach (PropertyInfo propertyInfo in employee.GetType().GetProperties())
            {
                Helpers.Print($"{propertyInfo.Name}:{propertyInfo.GetValue(employee)}");
            }
            Helpers.Print("=============================================");
        }

        public void PrintRoleData(List<Role>? roleData)
        {
            if (roleData is null)
            {
                Helpers.Print("No roles present in Data");
            }
            else
            {
                foreach (Role item in roleData)
                {
                    foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
                    {
                        Helpers.Print($"{propertyInfo.Name}:{propertyInfo.GetValue(item)}");
                    }
                    Helpers.Print("==========================================");
                }
            }
        }
    }
}