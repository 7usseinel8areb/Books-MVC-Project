using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books_MVC_Project.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Auto increment
        public int Id { get; set; }

        [Required,MaxLength(100)]
        public string Name { get; set; }

        public bool isActive { get; set; }
    }
}
