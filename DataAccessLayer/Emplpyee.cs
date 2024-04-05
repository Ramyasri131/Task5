﻿
namespace EmployeeDirectory.EmployeeData
{
    public class Employee
    {

        public string? Id { get; set; } 
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? Email { get; set; } 
        public string? MobileNumber { get; set; } 
        public string? DateOfBirth { get; set; } 
        public string? DateOfJoin { get; set; } 
        public string Location { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public string Project { get; set; } = string.Empty;
    }
}