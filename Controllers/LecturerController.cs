using Microsoft.AspNetCore.Mvc;
using cmcsapp.Data; // Namespace for your ApplicationDbContext
using System.Linq;
using System.Threading.Tasks;

namespace cmcsapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LecturerController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  allow lecturers to submit their details
        [HttpPost("SubmitLecturer")]
        public async Task<IActionResult> SubmitLecturer([FromBody] Lecturer lecturer)
        {
            if (lecturer == null || string.IsNullOrEmpty(lecturer.Name) || string.IsNullOrEmpty(lecturer.Email))
            {
                return BadRequest("Invalid lecturer data.");
            }

            // Add the lecturer to the in-memory database
            _context.Lecturers.Add(lecturer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Lecturer details submitted successfully.", lecturerId = lecturer.Id });
        }

        // Endpoint for lecturers to submit a claim
        [HttpPost("SubmitClaim")]
        public async Task<IActionResult> SubmitClaim([FromBody] Claim claim)
        {
            if (claim == null || claim.HoursWorked <= 0 || claim.HourlyRate <= 0)
            {
                return BadRequest("Invalid claim data.");
            }

            claim.TotalPayment = claim.HoursWorked * claim.HourlyRate;
            claim.SubmissionDate = System.DateTime.UtcNow;
            claim.Status ??= "Pending";

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Claim submitted successfully.", claimId = claim.Id });
        }

        // Endpoint to fetch all claims
        [HttpGet("GetAllClaims")]
        public IActionResult GetAllClaims()
        {
            var claims = _context.Claims.ToList();
            return Ok(claims);
        }

        // Endpoint to fetch claims by status
        [HttpGet("GetClaimsByStatus")]
        public IActionResult GetClaimsByStatus(string status)
        {
            var claims = _context.Claims.Where(c => c.Status == status).ToList();
            return Ok(claims);
        }
    }
}
