using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Models
{
    public class Nurse
    {
        //public Nurse()
        //{
        //    Patients = new HashSet<Patient>();
        //}
        public int NurseId { get; set; }
        public string NurseName { get; set; }
        public int? HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }


    }
}
