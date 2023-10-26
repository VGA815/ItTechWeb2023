using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItTechServer.Models
{
    public class ProjectModel
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int TargetsId { get; set; }
        [ForeignKey("TargetsId")]
        public List<TargetModel> Targets { get; set; } = new();
    }
}
