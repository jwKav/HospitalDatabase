using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Models
{
    public class PatientDoctorNurseHospitalView
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }

        [Display(Name = "D.O.B")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string DoctorName { get; set; }
        public string NurseName { get; set; }
        public string HospitalName { get; set; }
    }
}
