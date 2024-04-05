namespace EmployeeDirectory.RoleData
{
    public class Role
    {
        public required string Name { get; set; }
        public string location { get; set; } = string.Empty;
        public string department { get; set; } = string.Empty;
        public string? description { get; set; }
    }
}