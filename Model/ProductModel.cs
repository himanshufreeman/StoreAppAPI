using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace StoreAppAPI.Model
{
    [Index(nameof(ProductName), IsUnique = true)]
    public class ProductModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        public bool ProductStatus { get; set; } = true;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public long Count { get; set; }
        public long TotalSold { get; set; } = 0;
    }
}
