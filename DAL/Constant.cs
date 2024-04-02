namespace EmployeeDirectory.Constant
{
    public static class DepartmentList
    {
        public static Dictionary<int, string> departments = new Dictionary<int, string>
        {
          {1,"PE" },
          { 2,"QA" },
          {3,"Marketing" }
        };
    }

    public static class ProjectList
    {
        public static Dictionary<int, string> projects = new Dictionary<int, string>
        {
           {1,"Project1" },
           { 2,"Project2" },
           {3,"Project3" }
        };
    }

    public static class ManagerList
    {
        public static Dictionary<int, string> managers = new Dictionary<int, string>
        {
            {1,"Sandeep" },
            {2,"Siva" },
            {3,"Shashank" }
        };
    }

    public static class LoactionList
    {
        public static Dictionary<int, string> locations = new Dictionary<int, string>
        {
            {1,"Hyderabad" },
            { 2,"Banglore" },
            {3,"Vizag" }
        };
    }

    public static class RoleList
    {
        public static Dictionary<int, string> roles = new Dictionary<int, string>
        {
           {1,"Manager" },
           { 2,"Lead" },
           {3,"Intern" }
        };
    }
    public class EmployeeDetailsLabelList
    {
        public static Dictionary<int, string> employeeDataLabels = new Dictionary<int, string>
        {
           {1,"firstName" },
           {2,"lastName" },
           {3,"email" },
           {4,"mobileNumber" },
           {5, "dateOfBirth" },
           {6,"dateOfJoin" },
           {7,"location" },
           {8,"jobTitle" },
           {9,"department" },
           {10,"manager" },
           {11,"project" }
        };
    }

    public static class MenuList
    {
        public static Dictionary<int, string> MainMenu = new Dictionary<int, string>
        {
            {1,"Employee Management" },
            {2,"Role Management" },
            {3,"Exit" }
        };
        public static Dictionary<int, string> EmployeMaangementMenu = new Dictionary<int, string>
        {
            {1,"Add employee" },
            {2,"Display All" },
            {3,"Display One"},
            {4,"Edit employee"},
            {5,"Delete employee"},
            {6,"Go Back"}
        };
        public static Dictionary<int, string> RoleManagementMenu = new Dictionary<int, string>
        {
            {1,"Add Role" },
            {2,"Display All" },
            {3,"Go Back" }
        };

    }
    //class to store employee data 
    public class EmployeeData
    {
        public string empId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string mobileNumber { get; set; }
        public string dateOfBirth { get; set; }
        public string dateOfJoin { get; set; }
        public string location { get; set; }
        public string jobTitle { get; set; }
        public string department { get; set; }
        public string manager { get; set; }
        public string project { get; set; }
    }


    //class to store role data
    public class RoleData
    {
        public required string roleName { get; set; }
        public string location { get; set; }
        public string department { get; set; }
        public string description { get; set; }
    }

}