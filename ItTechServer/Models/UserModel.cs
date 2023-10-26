using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ItTechServer.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        //public int RoleId { get; set; }
        //public RoleModel Role { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public CompanyModel? Company { get; set; }
    }
}
