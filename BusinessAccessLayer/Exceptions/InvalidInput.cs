namespace EmployeeDirectory.BAL.Exceptions
{
    public class InvalidInput : Exception
    {
        public InvalidInput() : base(string.Format("Enter valid input")) { }
    }
}
