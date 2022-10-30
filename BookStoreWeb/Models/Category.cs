using System.ComponentModel.DataAnnotations;
namespace BookStoreWeb.Models
{
    public class Category
    {
        //autoincrement, Entity frameworks looks for Id and makes it pk and ai
        //if name is name differs add [Key]
        [Key]
        public int Id { get; set; }

        //is nullable
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [MaxLength(50, ErrorMessage = "${0} mag maximaal ${1} tekens bevatten")]
        [Display(Name = "Naam")]
        public string Name { get; set; }


        [Display(Name = "Volgnummer")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Aanmaakdatum")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
