using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Hospital_API.Models
{
    public partial class HospitalDbContext : DbContext
    {
        public HospitalDbContext()
        {
        }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Cleaner> Cleaners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("HospitalDb");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Doctor>().HasData(
                new Doctor { DoctorId = 2, DoctorName = "Dr. Ben Rao", Specialisation = "Oncologist", HospitalId = 1},
                new Doctor { DoctorId = 3, DoctorName = "Dr. Victoria Edwards", Specialisation = "Gynaecologist", HospitalId = 1},
                new Doctor { DoctorId = 4, DoctorName = "Dr. Tim Brooke", Specialisation = "Paediatrician", HospitalId = 1}
                );
            builder.Entity<Patient>().HasData(
                new Patient { PatientId = 1, PatientName = "Jordan Benbelaid", DoctorId = 2, DOB = new DateTime(1996, 10, 24), HospitalId = 1, NurseId = 1},
                new Patient { PatientId = 2, PatientName = "William Curran", DoctorId = 3, DOB = new DateTime(2011, 03, 11), HospitalId = 1, NurseId = 2},
                new Patient { PatientId = 3, PatientName = "Ryan Mason", DoctorId = 3, DOB = new DateTime(1990, 02, 10), HospitalId = 1, NurseId = 2}
                );
            builder.Entity<Hospital>().HasData(
                new Hospital { HospitalId = 1, HospitalName = "Queen's Hospital", City = "Romford", HospitalPostcode = "RM7 0AG", HospitalPhoneNumber = "0330 400 4333" },
                new Hospital { HospitalId = 2, HospitalName = "Moorfield's Hospital", City = "London", HospitalPostcode = "EC1V 2PD", HospitalPhoneNumber = "020 7253 3411" }

                );
            builder.Entity<Nurse>().HasData(
            new Nurse { NurseId = 1, NurseName = "Laura Swords", HospitalId = 1 },
            new Nurse { NurseId = 2, NurseName = "Stephen George", HospitalId = 1}
                );

            builder.Entity<Cleaner>().HasData(
                new Cleaner { CleanerId = 1, CleanerName = "Ciaran Dyer", HospitalId = 1 });


            builder.Entity<Patient>()
                .HasKey(p => p.PatientId);

            builder.Entity<Patient>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Patients)
                .HasForeignKey(p => p.DoctorId);
            builder.Entity<Patient>()
                .HasOne(p => p.Nurse)
                .WithMany(n => n.Patients)
                .HasForeignKey(p => p.NurseId);
            builder.Entity<Patient>()
                .HasOne(p => p.Hospital)
                .WithMany(h => h.Patients)
                .HasForeignKey(p => p.HospitalId);

            
            builder.Entity<Doctor>()
                .HasKey(d => d.DoctorId);
            builder.Entity<Doctor>()
                .HasOne(d => d.Hospital)
                .WithMany(h => h.Doctors)
                .HasForeignKey(d => d.HospitalId);


            builder.Entity<Nurse>()
                .HasKey(n => n.NurseId);
            builder.Entity<Nurse>()
                .HasOne(n => n.Hospital)
                .WithMany(h => h.Nurses)
                .HasForeignKey(n => n.HospitalId);

            builder.Entity<Hospital>()
                .HasKey(h => h.HospitalId);

            builder.Entity<Cleaner>()
                .HasKey(c => c.CleanerId);
                

        }
    }
}
