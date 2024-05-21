using EmployeeDirectory.DAL.Models;
using System.Reflection;

namespace EmployeeDirectory.Utilities
{
    public static class DisplayData
    {
        public static void PrintEmployeesData(List<Employee> employeeData)
        {
            foreach (Employee employee in employeeData)
            {
                PrintEmployeeData(employee);
            }
        }

        public static void PrintEmployeeData(Employee employee)
        {
            foreach (PropertyInfo propertyInfo in employee.GetType().GetProperties())
            {
                Display.Print($"{propertyInfo.Name}:{propertyInfo.GetValue(employee)}");
            }
            Display.Print("=============================================");
        }

        public static void PrintRoleData(List<Role>? roleData)
        {
            foreach (Role item in roleData!)
            {
                foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
                {
                    Display.Print($"{propertyInfo.Name}:{propertyInfo.GetValue(item)}");
                }
                Display.Print("==========================================");
            }
        }
    }
}