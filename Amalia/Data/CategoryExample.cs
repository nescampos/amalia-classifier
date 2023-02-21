using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amalia.Data
{
    public class CategoryExample
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [Required]
        public Category Category { get; set; }
    }
}
