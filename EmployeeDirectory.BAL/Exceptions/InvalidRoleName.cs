namespace EmployeeDirectory.BAL.Exceptions
{
    public class InvalidRoleName:Exception
    {
        public InvalidRoleName() : base(string.Format("Enter Valid Role Name")) { }
    }
}
