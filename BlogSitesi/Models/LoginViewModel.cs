using System.ComponentModel.DataAnnotations;

namespace BlogSitesi.Models
{
    public class LoginViewModel
    {
        [EmailAddress, Required(ErrorMessage = "{0} Alanı Boş Geçilemez!"), StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "Şifre"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!"), StringLength(50), MinLength(3), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
