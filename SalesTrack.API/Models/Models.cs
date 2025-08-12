using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesTrack.API.Models
{
    public class Country
    {
        [Key] public int CountryId { get; set; }
        [Required] public string CountryName { get; set; }
        public ICollection<City> Cities { get; set; }
    }

    public class City
    {
        [Key] public int CityId { get; set; }
        [Required] public string CityName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }

    public class Product
    {
        [Key] public int ProductId { get; set; }
        [Required] public string ProductName { get; set; }
    }

    [Table("Users")]
    public class SalesUser
    {
        [Key] public int UserId { get; set; }
        [Required] public string UserName { get; set; }
    }

    public class Sale
    {
        [Key] public int SaleId { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public SalesUser SalesUser { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
