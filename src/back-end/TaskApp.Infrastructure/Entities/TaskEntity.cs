using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskApp.Infrastructure.Entities
{
    [Table("Task")]
    public class TaskEntity : BaseEntity
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [ForeignKey("StatusId")]
        public int StatusId {  get; set; }
        public virtual StatusEntity Status { get; set; }
    }
}
