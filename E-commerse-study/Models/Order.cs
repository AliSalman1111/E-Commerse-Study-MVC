namespace E_commerse_study.Models
{
    public class Order
    {
       
            public int Id { get; set; }

            public string ApplicationUserId { get; set; }
            public ApplicationUser ApplicationUser { get; set; }

            public DateTime OrderDate { get; set; } = DateTime.Now;

            public decimal TotalAmount { get; set; }

        public string? status { get; set; } = "Pending";

            public ICollection<OrderItem> OrderItems { get; set; }
        }

    
}
