using DentalCenter.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCenter.Models
{
    public class Doctor
    {
        public string IdentityUserId { get; set; }

        public DentalCenterUser? IdentityUser { get; set; }
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

        [Display(Name = "Специальность")]
        public string? DoctorSpecialization { get; set; }

        [Display(Name ="Номер кабинета")]
        public int? DoctorCabinet { get; set; }

        [Display(Name = "Фотография")]
        public string? DoctorPhoto { get; set; }

        public List<Service>? Services { get; set; }
    }
}
