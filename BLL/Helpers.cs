namespace EmployeeDirectory.Utilities
{
    public class Helpers
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
        public void Print(int key, string message)
        {
            Console.WriteLine($"{key}.{message}");
        }
    }
}
