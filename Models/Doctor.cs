using System.ComponentModel.DataAnnotations;

namespace DentalCenter.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный e-mail")]
        [Required(ErrorMessage = "Введите e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Фамилия")]
        public string DoctorSurname { get; set; }

        [Display(Name = "Имя")]
        public string DoctorName { get; set; }

        [Display(Name = "Отчество")]
        public string DoctorPatronymic { get; set; }

        [Display(Name ="Номер кабинета")]
        public int? DoctorCabinet { get; set; }

        [Display(Name = "Фотография")]
        public string? DoctorPhoto { get; set; }
        
    }
}
