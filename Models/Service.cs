﻿using System.ComponentModel.DataAnnotations;

namespace DentalCenter.Models
{
    public class Service
    {
        public int ServiceId { get; set; }

        [Display(Name = "Название услуги")]
        public string ServiceName { get; set; }

        [StringLength(150)]
        [Display(Name = "Описание услуги")]
        public string ServiceDecription { get; set; }

        [Display(Name = "Цена услуги")]

        public int ServicePrice { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
