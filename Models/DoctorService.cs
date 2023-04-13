namespace DentalCenter.Models
{
    public class DoctorService
    {
        public int DoctorServiceId { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}
