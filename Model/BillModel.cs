using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreAppAPI.Model
{
    public class BillModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BillId { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set;}
        public DateTime BillTime { get; set;}= DateTime.Now;
        public decimal TotalAmount { get; set;}
        public string Bill { get; set;}
    }
}
