using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital_API.Models
{
    public class Patient
    {
        
        public int PatientId { get; set; }
        public string PatientName { get; set; }

        [Display(Name = "D.O.B")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public int? DoctorId { get; set; }
        public int? NurseId { get; set; }
        public int? HospitalId { get; set; }
        public Doctor Doctor { get; set; }
        public Nurse Nurse { get; set; }
        public Hospital Hospital { get; set; }
       
    }
}
