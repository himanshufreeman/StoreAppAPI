using System.ComponentModel.DataAnnotations;

namespace StoreAppAPI.DTOs
{
    public class BillDetailsDTO
    {
        [Key]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public int ProductCount { get; set; }
    }
}
