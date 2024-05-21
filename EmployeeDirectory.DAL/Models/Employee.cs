namespace EmployeeDirectory.DAL.Models
{
    public class Employee
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required long MobileNumber { get; set; }
        public required string DateOfBirth { get; set; }
        public required string DateOfJoin { get; set; }
        public required string Location { get; set; }
        public required string JobTitle { get; set; } 
        public required string Department { get; set; }
        public required string Manager { get; set; }
        public required string Project { get; set; } 
    }
}