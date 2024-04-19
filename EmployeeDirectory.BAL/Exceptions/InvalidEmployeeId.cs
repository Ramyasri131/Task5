namespace EmployeeDirectory.BAL.Exceptions
{
    public class InvalidEmployeeId : Exception
    {
        public InvalidEmployeeId(string message) : base(string.Format(message)) { }
    }
}
