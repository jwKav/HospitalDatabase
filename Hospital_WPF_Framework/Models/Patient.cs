using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_62_Hospital_WPF_Framework
{
    public class Patient
    {
        
        public int PatientId { get; set; }
        public string PatientName { get; set; }

        public DateTime DOB { get; set; }
        public int? DoctorId { get; set; }
        //public int? NurseId { get; set; }
        //public int? HospitalId { get; set; }
        public Doctor Doctor { get; set; }
        public Nurse Nurse { get; set; }
        public Hospital Hospital { get; set; }
    }
}
