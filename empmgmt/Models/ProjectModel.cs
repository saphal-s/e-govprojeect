using System.ComponentModel.DataAnnotations;

namespace empmgmt.Models
{
    public class ProjectModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        [Required]
        public string Details { get; set; }
    }
}
