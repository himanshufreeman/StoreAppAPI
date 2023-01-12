namespace StoreAppAPI.DTOs
{
    public class BillDTO
    {
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Bill { get; set; }
    }
}
