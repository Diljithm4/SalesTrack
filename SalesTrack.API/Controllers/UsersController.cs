using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesTrack.API.Data;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly SalesTrackDbContext _db;
    public UsersController(SalesTrackDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _db.Users.AsNoTracking().ToListAsync());
}
