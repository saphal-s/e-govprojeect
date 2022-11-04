using System.ComponentModel.DataAnnotations;

namespace empmgmt.Models
{
    public class PositionModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
