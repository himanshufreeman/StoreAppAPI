using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System;

namespace StoreAppAPI.Model
{
    public class CustomerModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        
        public long CustomerPhone { get; set; }
    }
}
