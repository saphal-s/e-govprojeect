using System.ComponentModel.DataAnnotations;

namespace empmgmt.Models
{
    public class DepartmentModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProjectId { get; set; }
    }
    public class DepartmentViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }

    }
}
