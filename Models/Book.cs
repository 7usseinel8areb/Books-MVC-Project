using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Books_MVC_Project.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Auto increment
        public int Id { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [MaxLength(100)]
        [Display(Name = "Book Title")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters are allowed.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters are allowed.")]
        public string Author { get; set; }

        [Required, MaxLength(2000)]
        public string? Description { get; set; }

        public DateTime AddedOn { get; set; }

        [ForeignKey("Category"),Required(ErrorMessage ="Please select the category of the book!")]
        [Display(Name = "Category")]
        public int Category_id { get; set; }
        public Category Category { get; set; }

        public Book()
        {
            AddedOn = DateTime.Now;
        }
    }
}
