using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItTechServer.Models
{
    public class TargetModel
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int GoalsId { get; set; }
        [ForeignKey("GoalsId")]
        public List<GoalModel> Goals { get; set; }
    }
}
