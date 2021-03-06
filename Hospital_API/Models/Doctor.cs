﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Models
{
    public class Doctor
    {
        //public Doctor()
        //{
        //    Patients = new HashSet<Patient>();
        //}
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Specialisation { get; set; }
        public int? HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int? PatientId { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
