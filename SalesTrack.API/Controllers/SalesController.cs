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
            .Select(s => new SaleDto
            {
                SaleId = s.SaleId,
                CountryId = s.CountryId,
                CountryName = s.Country.CountryName,
                CityId = s.CityId,
                CityName = s.City.CityName,
                ProductId = s.ProductId,
                ProductName = s.Product.ProductName,
                Quantity = s.Quantity,
                Amount = s.Amount,
                UserId = s.UserId,
                UserName = s.SalesUser.UserName,
                SaleDate = s.SaleDate
            })
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
            .Where(s => s.SaleId == id)
            .Select(s => new SaleDto
            {
                SaleId = s.SaleId,
                CountryId = s.CountryId,
                CountryName = s.Country.CountryName,
                CityId = s.CityId,
                CityName = s.City.CityName,
                ProductId = s.ProductId,
                ProductName = s.Product.ProductName,
                Quantity = s.Quantity,
                Amount = s.Amount,
                UserId = s.UserId,
                UserName = s.SalesUser.UserName,
                SaleDate = s.SaleDate
            })
            .FirstOrDefaultAsync();

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

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _db.Countries.AsNoTracking().ToListAsync();
        return Ok(countries);
    }

    [HttpGet("cities")]
    public async Task<IActionResult> GetCities()
    {
        var cities = await _db.Cities.AsNoTracking().ToListAsync();
        return Ok(cities);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _db.Users.AsNoTracking().ToListAsync();
        return Ok(users);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _db.Products.AsNoTracking().ToListAsync();
        return Ok(products);
    }
}
