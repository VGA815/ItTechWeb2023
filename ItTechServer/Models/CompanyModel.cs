using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItTechServer.Models
{
    public class CompanyModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<string> Users { get; set; } = new();

        public int ProjectsId { get; set; }
        [ForeignKey("ProjectsId")]
        public List<ProjectModel> Projects { get; set; } = new();
    }
}
