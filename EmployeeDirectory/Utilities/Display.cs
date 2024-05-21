namespace EmployeeDirectory.Utilities
{
    public static class Display
    {
        public static void Print(string message)
        {
            Console.WriteLine(message);
        }
        public static void Print(int key, string message)
        {
            Console.WriteLine($"{key}.{message}");
        }
    }
}