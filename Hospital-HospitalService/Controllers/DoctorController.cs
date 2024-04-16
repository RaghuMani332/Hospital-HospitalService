using Hospital_HospitalService.DTO;
using Hospital_HospitalService.ExceptionHandler;
using Hospital_HospitalService.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_HospitalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApplicationExceptionHandler]
   // [Authorize(Roles = "Admin")]
    public class DoctorController(IDoctorService service) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public IActionResult CreateDoctor(DoctorRequest request)
        {
            return Ok(service.CreateDoctor(request));
        }
        [HttpGet("{doctorId}")]
        public IActionResult GetDoctorById(int doctorId)
        {
            return Ok(service.GetDoctorById(doctorId));
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            return Ok(service.GetAllDoctors());
        }

        [HttpPut("{doctorId}")]
        public IActionResult UpdateDoctor(int doctorId, DoctorRequest request)
        {
            return Ok(service.UpdateDoctor(doctorId, request));
        }

        [HttpDelete("{doctorId}")]
        public IActionResult DeleteDoctor(int doctorId)
        {
            return Ok(service.DeleteDoctor(doctorId));
        }
        [HttpGet("specialization/{specialization}")]
        public IActionResult GetDoctorsBySpecialization(string specialization)
        {
            return Ok(service.GetDoctorsBySpecialization(specialization));
        }
    }
}
