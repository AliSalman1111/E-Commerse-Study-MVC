using System.ComponentModel.DataAnnotations;

namespace E_commerse_study.ViewModel
{
    public class ApplicatinUserVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmePassword { get; set; }

        public string Address { get; set; }

    }
}
