using Microsoft.AspNetCore.Identity;

namespace empmgmt.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public override string PhoneNumber { get; set; }
        public string Salary { get; set; }
        public override string Email { get; set; }
        public string DpartmentId { get; set; }
        public string PositionId { get; set; }
    }
    public  class ApplicationViewUser : IdentityUser
    {
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public override string PhoneNumber { get; set; }
        public string Salary { get; set; }
        public override string Email { get; set; }
        public string DpartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string PositionId { get; set; }
        public string PositionName { get; set; }

    }
}
