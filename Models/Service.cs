﻿using System.ComponentModel.DataAnnotations;

namespace DentalCenter.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        [Display(Name = "Название услуги")]
        public string ServiceName { get; set; }
        [Display(Name = "Цена услуги")]
        public int ServicePrice { get; set; }
    }
}