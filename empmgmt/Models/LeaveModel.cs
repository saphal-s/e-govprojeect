namespace empmgmt.Models
{
    public class LeaveModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Schedule { get; set; }
        public bool IsRead { get; set; }
        public bool Approve { get; set; }
        public string UserId { get; set; }
    }
    public class LeaveViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Schedule { get; set; }
        public bool IsRead { get; set; }
        public bool Approve { get; set; }
        public string UserId { get; set; }
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
