using System.ComponentModel.DataAnnotations;

namespace E_commerse_study.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public string Address { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
