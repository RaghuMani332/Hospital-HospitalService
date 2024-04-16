namespace Hospital_HospitalService.Entity
{
    public class DoctorEntity
    {
        public int DId { get; set; }
        public String  DoctorName { get; set; }
        public String DoctorEmail { get; set; }
        public int DeptId { get; set; }
        public String Specialization { get; set; }
        public string Qualifications { get; set; }

    }
}
