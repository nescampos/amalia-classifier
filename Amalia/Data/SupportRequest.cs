using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amalia.Data
{
    public class SupportRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string From { get;set; }

        [Required]
        public string To { get; set; }

        [Required]
        public DateTime CreatedAt { get;set;}

        public int? CategoryId { get;set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
