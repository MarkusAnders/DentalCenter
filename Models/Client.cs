using DentalCenter.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace DentalCenter.Models
{
	public class Client
	{
		public int Id { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный e-mail")]
        [Required(ErrorMessage = "Введите e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string ClientSurname { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
		public string ClientName { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        [Display(Name = "Отчество")]
        public string ClientPatronymic { get; set; }

        [Display(Name = "Номер телефона")]
        public string? ClientPhone { get; set; }

		[Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ClientDateBirth { get; set; }

		[Display(Name = "Место жительства")]
        [StringLength(50, ErrorMessage = "Поле не может иметь более 50 символов")]
        public string? ClientPlaceHome { get; set; }

        
        public DentalCenterUser? DentalCenterUser { get; set; }
        public string DentalCenterUserId { get; set; }
    }
}
