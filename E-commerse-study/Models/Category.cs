using System.ComponentModel.DataAnnotations;

namespace E_commerse_study.Models
{
	public class Category
	{
		public int Id { get; set; }


		[Required(ErrorMessage = "Name is required")]
		[MinLength(3)]
		[MaxLength(25)]
       // [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

		public ICollection<Product> Products { get; set; }

       
    }
}
