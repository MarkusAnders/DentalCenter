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
		public int ClientPhone { get; set; }

	}
}
