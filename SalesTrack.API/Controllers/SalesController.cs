using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesTrack.API.Data;
using SalesTrack.API.Models;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly SalesTrackDbContext _db;
    public SalesController(SalesTrackDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _db.Sales
            .Include(s => s.Product)
            .Include(s => s.City)
            .Include(s => s.Country)
            .Include(s => s.SalesUser)
            .AsNoTracking()
            .ToListAsync();
        return Ok(sales);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var sale = await _db.Sales
            .Include(s => s.Product)
            .Include(s => s.City)
            .Include(s => s.Country)
            .Include(s => s.SalesUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SaleId == id);

        if (sale == null) return NotFound();
        return Ok(sale);
    }

    public class CreateSaleDto
    {
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public DateTime SaleDate { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleDto dto)
    {
        var sale = new Sale
        {
            CountryId = dto.CountryId,
            CityId = dto.CityId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Amount = dto.Amount,
            UserId = dto.UserId,
            SaleDate = dto.SaleDate
        };

        _db.Sales.Add(sale);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = sale.SaleId }, sale);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(int id)
    {
        var sale = await _db.Sales.FindAsync(id);
        if (sale == null)
        {
            return NotFound();
        }

        _db.Sales.Remove(sale);
        await _db.SaveChangesAsync();

        return NoContent();
    }

}
