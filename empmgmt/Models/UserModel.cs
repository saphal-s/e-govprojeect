using Microsoft.AspNetCore.Identity;

namespace empmgmt.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DpartmentId { get; set; }
        public string PositionId { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
    }
    public class UserViewModel
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DpartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string PositionId { get; set; }
        public string PositionName { get; set; }
        public string RoleName { get; set; }
    }
}
