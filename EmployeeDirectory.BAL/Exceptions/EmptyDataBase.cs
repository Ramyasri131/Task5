namespace EmployeeDirectory.BAL.Exceptions
{ 
    public class EmptyDataBase:Exception
    {
        public EmptyDataBase() :base(string.Format("No Employee Present in DataBase")){ }
    }
}
