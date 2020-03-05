using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Models
{
    public class Hospital
    {
        //public Hospital()
        //{
        //    Patients = new HashSet<Patient>();
        //    Doctors = new HashSet<Doctor>();
        //    Nurses = new HashSet<Nurse>();
        //    Cleaners = new HashSet<Cleaner>();
        //}
        

        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string City { get; set; }
        public string HospitalPostcode { get; set; }

        public string HospitalPhoneNumber { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Nurse> Nurses { get; set; }
        public virtual ICollection<Cleaner> Cleaners { get; set; }


    }
}
