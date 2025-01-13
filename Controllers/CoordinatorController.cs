using Microsoft.AspNetCore.Mvc;
using cmcsapp.Data;

using System.Linq;
using System.Threading.Tasks;

namespace cmcsapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoordinatorController : ControllerBase
    {
        private readonly ApplicationDbContext? _context;

        public CoordinatorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("ApproveClaim/{id}")]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            var claim = _context!.Claims!.FirstOrDefault(c => c.Id == id);
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
            var claim = _context!.Claims?.FirstOrDefault(c => c.Id == id);
            if (claim == null)
            {
                return NotFound("Claim not found.");
            }

            claim.Status = "Rejected";
            await _context.SaveChangesAsync();
            return Ok("Claim rejected successfully.");
        }

        [HttpGet("GetPendingClaims")]
        public IActionResult GetPendingClaims()
        {
            var claims = _context!.Claims?.Where(c => c.Status == "Pending").ToList();
            return Ok(claims);
        }
    }
}
