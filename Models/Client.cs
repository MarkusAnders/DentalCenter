using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace DentalCenter.Models
{
	public class Client
	{
		public int ClientId { get; set; }

		[Display(Name = "Фамилия")]
        public string ClientSurname { get; set; }

		[Display(Name = "Имя")]
		public string ClientName { get; set; }

		[Display(Name = "Отчество")]
        public string ClientPatronymic { get; set; }

		[Display(Name = "Номер телефона")]
        [RegularExpression(@"^\+7 \(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Не верный формат номера телефона.")]
        public string? ClientPhone { get; set; }

		[Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ClientDateBirth { get; set; }

		[Display(Name = "Место жительства")]
        [StringLength(50, ErrorMessage = "Поле не может иметь более 50 символов")]
        public string? ClientPlaceHome { get; set; }

	}
}
