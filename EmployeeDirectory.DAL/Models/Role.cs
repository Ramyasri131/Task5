namespace EmployeeDirectory.DAL.Models
{
    public class Role
    {
        public required string? Name { get; set; }

        public string Location { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}