namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeService
    {
        public void GetEmployeeInput();
        public void DisplayEmployees();
        public void DisplayEmployee();
        public void EditEmployee();
        public void DeleteEmployee();
    }
}