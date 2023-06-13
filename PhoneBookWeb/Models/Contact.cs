using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PhoneBookWeb.Models
{
    public class Contact
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Фамилия")]
        public string? SecondName { get; set; }

        [Display(Name = "Имя")]
        [Required]
        public string? FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string? MiddleName { get; set; }

        [Display(Name = "Номер телефона")]
        [Required]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Описание")]
        public string? Description { get; set; }
    }
}