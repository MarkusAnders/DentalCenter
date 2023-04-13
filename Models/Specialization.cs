using System.ComponentModel.DataAnnotations;

namespace DentalCenter.Models
{
    public class Specialization
    {
        public int SpecializationId { get; set; }

        [Display(Name = "Название специальности")]
        [StringLength(25, ErrorMessage = "Поле не может иметь более 25 символов.")]
        public string SpecializationName { get; set; }

        [Display(Name = "Образование")]
        [StringLength(40, ErrorMessage = "Поле не может иметь более 40 символов.")]
        public string SpecializationEducation { get; set; }

        [Display(Name = "Опыт работы")]
        public int SpecializationWorkExp { get; set; }
    }
}
