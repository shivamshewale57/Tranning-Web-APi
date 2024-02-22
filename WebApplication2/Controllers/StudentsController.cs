using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/v1/studentttttt")]
    //[Route("api/[controller]")]

    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        
        public IActionResult GetAllStudssents()
        {
            string[] studentNames = new string[] { "SHIVAM", "VIKASsssss" };
            return Ok(studentNames);
        }
    }
}
