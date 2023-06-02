using System.ComponentModel;

namespace DentalCenter.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int Id { get; set; }
        [DisplayName("Клиент")]
        public Client? Client { get; set; }

        public int ServiceId { get; set; }
        [DisplayName("Услуга")]
        public Service? Service { get; set; }

        public int DoctorId { get; set; }
        [DisplayName("Доктор")]
        public Doctor? Doctor { get; set; }

        [DisplayName("Дата записи")]
        public DateTime AppointmentDateVisiting { get; set; }
    }
}
