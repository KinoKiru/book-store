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

        /// <summary>
        /// {0} = display name,
        /// {1} = minimum
        /// {2} = maximum
        /// </summary>
        [Display(Name = "Volgnummer")]
        [Range(minimum: 1, maximum: 100, ErrorMessage = "{0} moet tussen {1} en {2} liggen")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Aanmaakdatum")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
