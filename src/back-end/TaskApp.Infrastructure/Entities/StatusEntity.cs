using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskApp.Infrastructure.Entities
{
    [Table("Status")]
    public class StatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        
        public virtual ICollection<TaskEntity> Tasks { get; set; } = [];
    }
}
