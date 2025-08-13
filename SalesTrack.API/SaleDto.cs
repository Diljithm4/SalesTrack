public class SaleDto
{
    public int SaleId { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public DateTime SaleDate { get; set; }
}