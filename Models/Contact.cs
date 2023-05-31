using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DentalCenter.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Введите Фамилию")]
        public string ContactSurname { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Введите Имя")]
        public string ContactName { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Введите Номер телефона")]
        public string ContactPhone { get; set; }

        [Display(Name = "Сообщение")]
        [Required(ErrorMessage = "Введите Сообещние")]
        [StringLength(300, ErrorMessage = "Текст должен быть менее 300 символов")]
        public string ContactMessage { get; set; }
    }   
}
