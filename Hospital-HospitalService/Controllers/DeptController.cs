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
    public class DeptController(IDeptService dept) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult CreateDepartment(DeptRequest deptRequest)
        {
            return Ok(dept.CreateDept(deptRequest) +"Is Id for that dept");
        }
        [HttpGet("int")]
        public IActionResult getByDeptId(int id)
        {
            return Ok(dept.getByDeptId(id));
        }
        [HttpGet("ByName")]
        public IActionResult getByDeptName(String name)
        {
            return Ok(dept.getByDeptName(name));
        }
    }
}
