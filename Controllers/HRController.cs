using Microsoft.AspNetCore.Mvc;
using cmcsapp.Data;
using System.Linq;
using System.Threading.Tasks;

namespace cmcsapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HRController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HRController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all approved claim
        [HttpGet("GetApprovedClaims")]
        public List<Claim> GetApprovedClaims()
        {
            return _context!.Claims!.Where(c => c.Status == "Approved").ToList();
        }

        // Get all lecturers
        [HttpGet("GetLecturers")]
        public List<Lecturer> GetLecturers()
        {
            return _context.Lecturers.ToList();
     
        }

        // Update lecturer details
        [HttpPut("UpdateLecturer/{id}")]
        public async Task<IActionResult> UpdateLecturer(int id, [FromBody] Lecturer updatedLecturer)
        {
            var lecturer = _context.Lecturers.FirstOrDefault(l => l.Id == id);
            if (lecturer == null)
            {
                return NotFound("Lecturer not found.");
            }

            lecturer.Name = updatedLecturer.Name;
            lecturer.Email = updatedLecturer.Email;
            lecturer.Department = updatedLecturer.Department;

            await _context.SaveChangesAsync();
            return Ok("Lecturer details updated successfully.");
        }

        [HttpPut("ApproveClaim/{id}")]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim == null)
            {
                return NotFound("Claim not found.");
            }

            claim.Status = "Approved";
            await _context.SaveChangesAsync();
            return Ok("Claim approved successfully.");
        }

        [HttpPut("RejectClaim/{id}")]
        public async Task<IActionResult> RejectClaim(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim == null)
            {
                return NotFound("Claim not found.");
            }

            claim.Status = "Rejected";
            await _context.SaveChangesAsync();
            return Ok("Claim rejected successfully.");
        }

        [HttpGet("GetPendingClaims")]
        public List<Claim> GetPendingClaims()
        {
            return _context.Claims.Where(c => c.Status == "Pending").ToList();
        }
    }
}
