using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class CoverType
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Soort Kaft")]
        [MaxLength(50, ErrorMessage = "${0} mag maximaal ${1} tekens bevatten")]
        public string Name { get; set; }
    }
}
