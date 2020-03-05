#define API
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_API.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Hospital_MVC.Controllers
{

    public class PatientsController : Controller
    {
        private readonly HospitalDbContext _context;

        public PatientsController(HospitalDbContext context)
        {
            _context = context;
        }


        private static Uri urlP = new Uri("https://localhost:44312/api/patients/");
        private static Uri urlD = new Uri("https://localhost:44312/api/doctors/");
        private static Uri urlN = new Uri("https://localhost:44312/api/nurses/");
        private static Uri urlH = new Uri("https://localhost:44312/api/hospitals/");
#if API      

        public async Task<IActionResult> Index()
        {
            List<PatientDoctorNurseHospitalView> ViewList = new List<PatientDoctorNurseHospitalView>();
            using (var client = new HttpClient())
            {

                var jsonStringP = await client.GetStringAsync(urlP.ToString());
                var patients = JsonConvert.DeserializeObject<List<Patient>>(jsonStringP);

                foreach (var patient in patients)
                {
                    var jsonStringD = await client.GetStringAsync(urlD.ToString() + patient.DoctorId);
                    var doctor = JsonConvert.DeserializeObject<Doctor>(jsonStringD);

                    var jsonStringN = await client.GetStringAsync(urlN.ToString() + patient.NurseId);
                    var nurse = JsonConvert.DeserializeObject<Nurse>(jsonStringN);

                    var jsonStringH = await client.GetStringAsync(urlH.ToString() + patient.HospitalId);
                    var hospital = JsonConvert.DeserializeObject<Hospital>(jsonStringH);

                    ViewList.Add(new PatientDoctorNurseHospitalView { 
                        PatientId = patient.PatientId,PatientName = patient.PatientName, DOB = patient.DOB,  
                        DoctorName = doctor.DoctorName,
                        NurseName = nurse.NurseName,
                        HospitalName = hospital.HospitalName });

                }
                    
                return View(ViewList);
            }

        }
        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string jsonString = null;
            if (id == null)
            {
                return NotFound();
            }
            using (var client = new HttpClient())
            {
                jsonString = await client.GetStringAsync(urlP.ToString() + id);
                var patient = JsonConvert.DeserializeObject<Patient>(jsonString);
                if (patient == null)
                {
                    return NotFound();
                }
                return View(patient);
            }

        }
        // GET: Patients/Create
        public IActionResult Create()
        {
            using (var client = new HttpClient())
            {
                ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
                ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId");
                ViewData["NurseId"] = new SelectList(_context.Nurses, "NurseId", "NurseId");
                return View();
            }
            
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,PatientName,DOB,DoctorId,NurseId,HospitalId")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    string patientAsJson = JsonConvert.SerializeObject(patient);
                    var httpContent = new StringContent(patientAsJson);
                    httpContent.Headers.ContentType.MediaType = "application/json";
                    httpContent.Headers.ContentType.CharSet = "UTF-8";
                    await client.PostAsync(urlP, httpContent);
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", patient.DoctorId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId", patient.HospitalId);
            ViewData["NurseId"] = new SelectList(_context.Nurses, "NurseId", "NurseId", patient.NurseId);
            return View(patient);
        }

#else
        

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.Patients.Include(p => p.Doctor).Include(p => p.Hospital).Include(p => p.Nurse);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .Include(p => p.Doctor)
                .Include(p => p.Hospital)
                .Include(p => p.Nurse)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId");
            ViewData["NurseId"] = new SelectList(_context.Nurses, "NurseId", "NurseId");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,PatientName,DOB,DoctorId,NurseId,HospitalId")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", patient.DoctorId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId", patient.HospitalId);
            ViewData["NurseId"] = new SelectList(_context.Nurses, "NurseId", "NurseId", patient.NurseId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", patient.DoctorId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId", patient.HospitalId);
            ViewData["NurseId"] = new SelectList(_context.Nurses, "NurseId", "NurseId", patient.NurseId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,PatientName,DOB,DoctorId,NurseId,HospitalId")] Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", patient.DoctorId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId", patient.HospitalId);
            ViewData["NurseId"] = new SelectList(_context.Nurses, "NurseId", "NurseId", patient.NurseId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .Include(p => p.Doctor)
                .Include(p => p.Hospital)
                .Include(p => p.Nurse)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }
#endif

    }
}
    
