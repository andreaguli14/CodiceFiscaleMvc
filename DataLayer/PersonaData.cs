using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DataLayer;
public class PersonaDataViewModel
{

    [Display(Name = "Nome")]
    [Required]

    [StringLength(20, MinimumLength = 3, ErrorMessage = "Il nome deve essere compreso tra 3 e 20 caratteri")]

    public string FirstName { get; set; }
    [Display(Name = "Cognome")]
    [Required]


    public string LastName { get; set; }
    [Display(Name = "Data di Nascita: ")]
    [DataType(DataType.Date)]

    public DateTime Birthday { get; set; }
    [Display(Name = "Comune di: ")]

    public string Istat { get; set; }
    [Display(Name = "Genere")]

    public char Gender { get; set; }
    public string CodiceFiscale { get; set; }

}

