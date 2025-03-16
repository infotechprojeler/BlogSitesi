using System.ComponentModel.DataAnnotations;

namespace BlogSitesi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Adı"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Soyadı"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!"), StringLength(50)]
        public string Surname { get; set; }
        [EmailAddress, Required(ErrorMessage = "{0} Alanı Boş Geçilemez!"), StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "Şifre"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!"), StringLength(50), MinLength(3)]
        public string Password { get; set; }
        [Display(Name = "Kullanıcı Adı"), StringLength(50)]
        public string? Username { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; }
    }
}
